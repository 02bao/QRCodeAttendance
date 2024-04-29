namespace QRCodeAttendance.Application.User;

public interface IUserService
{
    Task<UserAuthenticate> Login(string Email, string Password);
    Task<bool> Delete(long id);
    Task<bool> Create(string Email, string FullName, string Password, bool IsWoman, long RoleId);
    Task<List<UserDTO>> GetAll();
    Task<UserDTO>? GetById(long Id);
    Task<List<UserDTO>> GetByPositionId(long PositionId);
}