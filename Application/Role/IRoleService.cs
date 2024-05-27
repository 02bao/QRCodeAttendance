namespace QRCodeAttendance.Application.Role;

public interface IRoleService
{
    Task<bool> CreateNewRole( string Name);
    Task<List<RoleDTO>> GetAll();
    Task<RoleDTO> GetById(long Id);
    Task<bool> Update(long Id, string Name);
    Task<bool> Delete(long Id);
    Task<bool> ChangeRoleForUser(long UserId,long RoleId);
}
