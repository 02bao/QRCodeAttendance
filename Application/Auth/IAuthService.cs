
namespace QRCodeAttendance.Application.Auth;

public interface IAuthService
{
    Task<UserAuthenticate> Login(string Email, string Password);
}