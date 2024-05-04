namespace QRCodeAttendance.Domain.Entities;

public class SqlWorkingHours
{
    public long Id { get; set; }
    public long PositionId { get; set; }
    public SqlPosition Position { get; set; } = null!;
    public TimeSpan StartTime { get; set; } 
    public TimeSpan EndTime { get; set; }
}
