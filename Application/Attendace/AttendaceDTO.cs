using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Attendace;

public class AttendaceDTO
{
    public long Id { get; set; }
    public TimeSpan CheckInTime { get; set; } = DateTime.UtcNow.TimeOfDay;
    public bool IsPresent { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public AttendaceStatus Status { get; set; } = AttendaceStatus.OnTime;
    public long CompanyId { get; set; }
    public List<DepartmentGetAttendance> Departments { get; set; } = new List<DepartmentGetAttendance>();
}
