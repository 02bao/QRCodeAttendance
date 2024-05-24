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
    public async Task<IActionResult> GetByUserId(long Id)
    {
        AttendanceGetByUser? dto = await _attendaceService.GetByUserId(Id);
        return Ok(dto);
    }
}
