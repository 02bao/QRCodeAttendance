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
            Status = entity.Status
        };
    }


}
