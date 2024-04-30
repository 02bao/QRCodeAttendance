namespace QRCodeAttendance.Application.User;

public interface IUserService
{
    Task<bool> Delete(long id);
    Task<string> Create(string Email, string FullName,string Phone, string Password, bool IsWoman, long RoleId);
    Task<bool> VerifyUser(string Token);
    Task<List<UserDTO>> GetAll();
    Task<UserDTO?> GetById(long Id);
    Task<List<UserDTO>> GetUsersByPositionId(long PositionId);
    Task<bool> Update(long UserId, string Email, string FullName,string Phone, bool IsWoman, long RoleId, List<IFormFile> Images);
}