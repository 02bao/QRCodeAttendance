using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Notification;
using QRCodeAttendance.Domain.Entities;
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

    [HttpPut("{Id}")]
    public async Task<IActionResult> HasRead(long Id)
    {
       bool IsSuccess = await _notificationService.HasRead(Id);
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _notificationService.Delete(Id);
        return IsSuccess ? Ok() : BadRequest();
    }
}
