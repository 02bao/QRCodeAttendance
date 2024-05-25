namespace QRCodeAttendance.Domain.Entities;

public class SqlNotification
{
    public long Id { get; set; }
    public string Message { get; set; } = "";
    public bool IsRead { get; set; } = false;
    public SqlUser User { get; set; } = new SqlUser();
    public DateTime CreateAt { get; set; } = DateTime.UtcNow.Date;
}
