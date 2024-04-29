﻿namespace QRCodeAttendance.Application.Position;

public interface IPositionService
{
    Task<bool> CreateNewPositions(long DepartmentId, string Name, string Description);
    Task<List<PositionDTO>> GetAll();
    Task<PositionDTO?> GetById(long Id);
    Task<List<PositionDTO>> GetByDepartmentId(long DepartmentId);
    Task<bool> Update(long PositionId, string? Name, string? Description);
    Task<bool> Delete(long Id);

    Task<bool> AssignUserToPosition(long PositionId, long UserId);
    Task<bool> RemoveUserFromPosition(long PositionId, long UserId);
}
