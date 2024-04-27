using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QRCodeAttendance.QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.QRCodeAttendance.Infrastructure.Constant;
using QRCodeAttendance.QRCodeAttendance.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QRCodeAttendance.QRCodeAttendance.Application.Token;
public class TokenService(DataContext context) : ITokenService
{
    public async Task<TokenItem> GenerateToken(SqlUser user, string role)
    {
        try
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(ConfigKey.JWT_KEY);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                        new Claim("id", user.Id.ToString()),
                        new Claim("email", user.Email),
                        new Claim("role", role),
                    }),
                Issuer = ConfigKey.VALID_ISSUER,
                Audience = ConfigKey.VALID_AUDIENCE,
                Expires = ConfigKey.getATExpiredTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                    (secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            SqlToken createTokenResult = await CreateToken(accessToken, refreshToken, user.Id);

            if (string.IsNullOrEmpty(createTokenResult.AccessToken))
            {
                return new TokenItem();
            }

            return new TokenItem
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new TokenItem();
        }
    }
    public TokenDecodedDTO DecodeToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(ConfigKey.JWT_KEY);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = ConfigKey.VALID_AUDIENCE,
                ValidIssuer = ConfigKey.VALID_ISSUER,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };

            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            TokenDecodedDTO tokenInfo = new()
            {
                Id = long.Parse(principal.FindFirst("id")?.Value ?? "-1"),
                Email = principal.Claims
                .First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                ?.Value ?? "",
                Role = principal.Claims
                .First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                ?.Value ?? "",
            };

            return tokenInfo;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"func: DecodeToken -> with token: {token} -> failed , Exception: {ex.InnerException}");
            return new TokenDecodedDTO();
        }
    }

    #region helper function
    private string GenerateRefreshToken()
    {
        var random = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }
    }
    public async Task<SqlToken> CreateToken(string accessToken, string refreshToken, long userId)
    {
        try
        {
            var sqlUser = context.Users.Where(s => s.Id.CompareTo(userId) == 0)
                                    .Include(s => s.Tokens)
                                    .FirstOrDefault();
            if (sqlUser is null)
            {
                return new SqlToken();
            }
            SqlToken? existingToken = context.Tokens.Where(s => s.AccessToken.CompareTo(accessToken) == 0 &&
                                               s.RefreshToken.CompareTo(refreshToken) == 0 &&
                                               s.IsExpired == false)
                                   .FirstOrDefault();
            if (existingToken is not null)
            {
                return new SqlToken();
            }

            SqlToken token = new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                CreateTime = DateTime.UtcNow,
                ExpiredTime = ConfigKey.getRTExpiredTime(),
                IsExpired = false,
                UserId = userId
            };

            context.Tokens.Add(token);

            int rowsAffected = await context.SaveChangesAsync();

            return token;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new SqlToken();
        }
    }
    #endregion
}
