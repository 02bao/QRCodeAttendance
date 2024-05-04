namespace QRCodeAttendance.Domain.Entities;

public class SqlShift
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
