using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlAttendace
{
    [Key]
    public long Id { get; set; }
    public TimeSpan CheckInTime { get; set; } = DateTime.UtcNow.TimeOfDay;
    public bool IsPresent { get; set; } = true;
    public SqlCompany Company { get; set; } = new SqlCompany();
    public SqlUser User { get; set; } = new SqlUser();
    public SqlDepartment Department { get; set; } = new SqlDepartment();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public AttendaceStatus Status { get; set; } = AttendaceStatus.OnTime;
}


public enum AttendaceStatus
{
    OnTime,
    Late,
    Absent
}
