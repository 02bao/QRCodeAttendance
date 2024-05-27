using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Application.Notification;

public class NotificationService(
    DataContext _context) : INotificationService
{
    public async Task<bool> Delete(long Id)
    {
        SqlNotification? noti = await _context.Notifications
            .Where(s => s.Id == Id)
            .FirstOrDefaultAsync();
        if(noti == null) { return false; }
        _context.Notifications.Remove(noti);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<NotificationDTO>> GetNotiInDay(DateTime date)
    {
        DateTime Startdate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
        List<SqlNotification>? notifications = await _context.Notifications
            .Where(s => s.CreateAt == Startdate.Date)
            .ToListAsync();
        List<NotificationDTO>? dto = notifications.Select(s => s.ToDTO()).ToList();
        return dto;
    }

    public async Task<bool> HasRead(long NotificationId)
    {
        SqlNotification? noti = await _context.Notifications
            .Where(s => s.Id == NotificationId && 
                        s.IsRead == false)
            .FirstOrDefaultAsync();
        if(noti == null) { return false; }
        noti.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }

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
