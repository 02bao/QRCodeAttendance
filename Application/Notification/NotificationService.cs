using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Application.Notification;

public class NotificationService(
    DataContext _context) : INotificationService
{
    public async Task<bool> NotifyCheckIn(SqlUser user, DateTime checkInTime)
    {
        await CreateNewNoti(user, checkInTime);
        return true;
    }

    private async Task CreateNewNoti(SqlUser user, DateTime checkInTime)
    {
        string message = $"{user.FullName} đã điểm danh lúc {checkInTime:yyyy-MM-dd HH:mm:ss} UTC.";
        SqlNotification NewNoti = new()
        {
            Message = message,
            CreateAt = DateTime.UtcNow.Date,
            IsRead = false,
            User = user,
        };
        _context.Notifications.Add(NewNoti);
        await _context.SaveChangesAsync();
    }
}
