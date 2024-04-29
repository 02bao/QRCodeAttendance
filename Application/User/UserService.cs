using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Application.Token;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;
using Serilog;

namespace QRCodeAttendance.Application.User;

public class UserService(DataContext _context,
    ITokenService _tokenService) : IUserService
{
    public async Task<UserAuthenticate> Login(string Email, string Password)
    {
        try
        {
            SqlUser? user = _context.Users
                .Where(s => s.Email.CompareTo(Email) == 0 &&
                            s.Password.CompareTo(Password) == 0 &&
                            s.IsDeleted == false)
                .Include(s => s.Role)
                .FirstOrDefault();

            if (user == null)
            {
                return new UserAuthenticate();
            }

            TokenItem token = await _tokenService.GenerateToken(user, user.Role.Name);

            if (string.IsNullOrEmpty(token.AccessToken)
                || string.IsNullOrEmpty(token.RefreshToken))
            {
                return new UserAuthenticate();
            }

            UserAuthenticate item = new()
            {
                Id = user.Id,
                Email = Email,
                Role = user.Role.Name,
                Token = token
            };

            return item;
        }
        catch (Exception ex)
        {
            Log.Error($"func: login - failed with username: {Email} and password: {Password} - Exception : {ex.InnerException}");
            return new UserAuthenticate();
        }
    }


    public async Task<bool> Delete(long id)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == id &&
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Create(string Email, string FullName, string Password, bool IsWoman, long RoleId)
    {
        SqlUser? user = await _context.Users.Where(s => s.Email.CompareTo(Email) == 0).FirstOrDefaultAsync();
        if (user != null)
        {
            return false;
        }

        SqlRole? role = await _context.Roles.Where(s => s.Id == RoleId).FirstOrDefaultAsync();
        if (role == null)
        {
            return false;
        }

        user = new SqlUser
        {
            Email = Email,
            FullName = FullName,
            Password = Password,
            IsWoman = IsWoman,
            Role = role
        };
        await _context.SaveChangesAsync();
        return true;
    }
}
