using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Notification;

public interface INotificationService
{
    Task<bool> NotifyCheckIn(SqlUser user, DateTime checkInTime);
    Task<List<NotificationDTO>> GetNotiInDay(DateTime date);
    Task<bool> HasRead(long NotificationId);
    Task<bool> Delete(long Id);
    Task<bool> DeleteAllNoti();
    Task<bool> AllNotiHasRead();
    Task<bool> DeleteAllNotiHasRead();
}
