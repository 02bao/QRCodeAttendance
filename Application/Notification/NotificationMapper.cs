using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Notification;

public static class NotificationMapper
{
    public static NotificationDTO ToDTO(this SqlNotification entity)
    {
        return new NotificationDTO
        {
            Id = entity.Id,
            Message = entity.Message,
            IsRead = entity.IsRead,
            CreateAt = entity.CreateAt
        };
    }
}
