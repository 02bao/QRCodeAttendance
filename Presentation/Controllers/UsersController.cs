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
    [Role("Admin")]
    public async Task<IActionResult> CreateUser(UserCreateModel Model)
    {
        bool IsSuccess = await _userService.Create(Model.Email, Model.FullName, Model.Password, Model.IsWoman, Model.RoleId);
        return IsSuccess ? Ok(Model) : BadRequest();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        List<UserDTO> dtos = await _userService.GetAll();
        return Ok(dtos);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult>? GetById(long Id)
    {
        UserDTO? dto = await _userService.GetById(Id);
        if (dto == null)
        {
            return NotFound();
        }
        return Ok(dto);
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
        bool IsSuccess = await _positionService.AssignUserToPosition(Id, PositionId);
        return IsSuccess ? Ok(Id) : BadRequest();
    }

    [HttpPut("{Id}/Positions/{PositionId}/remove")]
    public async Task<IActionResult> RemoveUserFromPosition(long Id, long PositionId)
    {
        bool IsSuccess = await _positionService.RemoveUserFromPosition(Id, PositionId);
        return IsSuccess ? Ok(Id) : BadRequest();
    }
}