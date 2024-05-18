using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Attendace;

public static class AttendanceMapper
{
    public static AttendaceDTO ToDTO(this SqlAttendace entity)
    {
        return new AttendaceDTO
        {
            Id = entity.Id,
            CheckInTime = entity.CheckInTime,
            IsPresent = entity.IsPresent,
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            CompanyId = entity.Company.Id,
            Departments = entity.Department.User
            .GroupBy(user => user.Department)
            .Select(group => group.FirstOrDefault().Department.ToAttendanceDTO())
            .Where(department => department != null)
            .ToList()
        };
    }
}
