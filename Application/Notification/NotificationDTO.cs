using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Notification;

public class NotificationDTO
{
    public long Id { get; set; }
    public string Message { get; set; } = "";
    public bool IsRead { get; set; } = false;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow.Date;
}

