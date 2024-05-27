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

    [HttpPut("AllNotiHasRead")]
    public async Task<IActionResult> AllNotiHasRead()
    {
        bool IsSuccess = await _notificationService.AllNotiHasRead();
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _notificationService.Delete(Id);
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteAllNoti()
    {
        bool IsSuccess = await _notificationService.DeleteAllNoti();
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpDelete("AllNotiHasRead")]
    public async Task<IActionResult> DeleteAllNotiHasRead()
    {
        bool IsSuccess = await _notificationService.DeleteAllNotiHasRead();
        return IsSuccess ? Ok() : BadRequest();
    }

}
