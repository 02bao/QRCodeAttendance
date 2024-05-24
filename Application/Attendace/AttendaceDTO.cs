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
    public string StatusString
    {
        get
        {
            return Status switch
            {
                AttendaceStatus.OnTime => "Ontime",
                AttendaceStatus.Late => "Late",
                AttendaceStatus.Absent => "Absent",
                _ => "Unknown"
            };
        }
    }
}

public class AttendanceGetByUser
{
    public string UserName { get; set; } = "";
    public List<AttendaceDTO> Attendaces { get; set; } = new List<AttendaceDTO>();
}

