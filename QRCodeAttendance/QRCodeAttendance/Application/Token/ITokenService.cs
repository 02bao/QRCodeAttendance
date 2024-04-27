using QRCodeAttendance.QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.QRCodeAttendance.Application.Token;
public interface ITokenService
{
    Task<SqlToken> CreateToken(string accessToken, string refreshToken, long userId);
    TokenDecodedDTO DecodeToken(string token);
    Task<TokenItem> GenerateToken(SqlUser user, string role);
}