using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Presentation.Models;

public class NotificationModel
{
}

public class NotificationCreateModel
{
    public string Message { get; set; } = "";
    public bool IsRead { get; set; } = false;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow.Date;
}
