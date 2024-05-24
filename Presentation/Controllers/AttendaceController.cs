using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Attendace;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class AttendaceController(
    IAttendaceService _attendaceService) : BaseController
{
    [HttpPost("")]
    public async Task<IActionResult> CheckIn(AttendanceCheckInModel Model)
    {
        bool IsSuccess = await _attendaceService.CheckIn(Model.UserId, Model.CompanyId);
        return IsSuccess? Ok() : BadRequest();
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetByUserId(long Id, DateTime date)
    {
        AttendanceGetByUser? dto = await _attendaceService.GetByUserId(Id, date);
        return Ok(dto);
    }

    [HttpGet("UserInMonth/{Id}")]
    public async Task<IActionResult> GetByUserIdInMonth(long Id, string month)
    {
        AttendanceGetByUserInMonth? dto = await _attendaceService.GetByUserInMonth(Id, month);
        return Ok(dto);
    }
}
