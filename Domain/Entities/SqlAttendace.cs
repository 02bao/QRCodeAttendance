namespace QRCodeAttendance.Domain.Entities;

public class SqlAttendace
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public SqlUser User { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime CheckInTime { get; set; } = DateTime.UtcNow;
    public DateTime CheckOutTime { get; set; } = DateTime.UtcNow;
    public AttendanceStatus Status { get; set; }
}

public enum AttendanceStatus
{
    CheckedIn =1,
    NotCheckedIn=2,
    Late=3,
    EarlyLeave=4
}
