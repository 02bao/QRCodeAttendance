using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Notification;
using QRCodeAttendance.Presentation.Filters;

namespace QRCodeAttendance.Presentation.Controllers;

public class NotificationController(
    INotificationService _notificationService) : BaseController
{
    [HttpGet("")]
    [Role("Admin")]
    public async Task<IActionResult> GetNotiInDay(DateTime date)
    {
        List<NotificationDTO> dto = await _notificationService.GetNotiInDay(date);
        return Ok(dto);
    }
}
