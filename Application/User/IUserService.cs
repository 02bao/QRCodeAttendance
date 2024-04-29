namespace QRCodeAttendance.Application.User;

public interface IUserService
{
    Task<bool> Register(string AdminToken,string Email, string Fullname, string Password, bool IsWomen, long RoleId);
    Task<UserAuthenticate> Login(string Email, string Password);
    Task<bool> Delete(long id);
    Task<bool> Create(string Email, string FullName, string Password, bool IsWoman, long RoleId);
}