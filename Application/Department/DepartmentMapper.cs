using QRCodeAttendance.Application.User;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Department;

public static class DepartmentMapper
{
    public static DepartmentItemDTO ToDTO(this SqlDepartment entity)
    {
        return new DepartmentItemDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            PositionCount = entity.Positions.Where(s => s.IsDeleted == false).Count(),
            EmployeeCount = entity.User.Where(s => s.IsDeleted == false).Count(),
        };
    }

    public static DepartmentGetAttendance ToAttendanceDTO(this SqlDepartment entity)
    {
        return new DepartmentGetAttendance
        {
            Id = entity.Id,
            Name = entity.Name,
            User = entity.User.Select(s => s.ToAttendaceDTO()).ToList(),
        };
    }
}
