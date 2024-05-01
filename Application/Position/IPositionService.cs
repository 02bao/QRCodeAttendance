using QRCodeAttendance.Application.User;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Position;

public interface IPositionService
{
    Task<bool> CreateNewPositions(long DepartmentId, string Name, string Description);
    Task<List<PositionDTO>> GetAll();
    Task<PositionDTO?> GetById(long Id);
    Task<List<PositionDTO>> GetPositionsByDepartmentId(long DepartmentId);
    Task<bool> Update(long PositionId, string? Name, string? Description);
    Task<bool> Delete(long Id);
    Task<List<UserDTO>> GetUserWithoutPosition();
    Task<bool> AssignUserToPosition(long UserId, long PositionId);
    Task<bool> RemoveUserFromPosition(long UserId, long PositionId);
}
