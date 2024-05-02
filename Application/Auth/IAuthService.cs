
namespace QRCodeAttendance.Application.Auth;

public interface IAuthService
{
    Task<UserAuthenticate> Login(string Email, string Password);
    Task<bool> ChangePassword(long UserId, string OldPwd, string NewPwd);
    Task<bool> ResetPassword(long UserId, string NewPwd);
}