using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Presentation.Filters;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class UsersController(
    IUserService _userService,
    IPositionService _positionService) : BaseController
{
    [HttpPost("")]
    public async Task<IActionResult> Register(UserCreateModel Model)
    {
        bool IsSuccess = await _userService.Create(Model.Email, Model.FullName, Model.Password, Model.IsWoman, Model.RoleId);
        return IsSuccess ? Ok(Model) : BadRequest();
    }

    [HttpDelete("{id}")]
    [Role("Admin")]
    public async Task<IActionResult> Delete(long id)
    {
        bool success = await _userService.Delete(id);
        if (success)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut("{Id}/Positions/{PositionId}/assign")]
    public async Task<IActionResult> AssignUserToPosition(long Id, long PositionId)
    {
        bool IsSuccess = await _positionService.AssignUserToPosition(PositionId, Id);
        return IsSuccess ? Ok(Id) : BadRequest();
    }

    [HttpPut("{Id}/Positions/{PositionId}/remove")]
    public async Task<IActionResult> RemoveUserFromPosition(long Id, long PositionId)
    {
        bool IsSuccess = await _positionService.RemoveUserFromPosition(Id, PositionId);
        return IsSuccess ? Ok(Id) : BadRequest();
    }
}