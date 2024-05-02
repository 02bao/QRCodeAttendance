using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Email;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Presentation.Filters;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class UsersController(
    IUserService _userService,
    IPositionService _positionService,
    IEmailService _emailService) : BaseController
{
    [HttpPost("")]
    [Role("Admin")]
    public async Task<IActionResult> CreateUser(UserCreateModel Model)
    {
        string Token = await _userService.Create(Model.Email, Model.FullName, Model.Phone, Model.Password, Model.IsWoman, Model.RoleId);
        if (Token == "") { return BadRequest(); }
        bool EmailSend = _emailService.SendRegisterEmail(Model.Email, Model.FullName, Token);
        if (!EmailSend) { Console.WriteLine("Failed to send registration email to: " + Model.Email); }
        return EmailSend ? Ok() : BadRequest();
    }
    [HttpGet("Verify/{Token}")]
    public async Task<IActionResult> Verify(string Token)
    {
        bool IsSuccess = await _userService.VerifyUser(Token);
        return IsSuccess ? Ok("Verify Successfully") : BadRequest();
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
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(long Id, UserUpdateModel Model)
    {

        bool IsSuccess = await _userService.Update(Id, Model.Email, Model.Phone, Model.FullName, Model.IsWoman, Model.RoleId, Model.FileId);
        return IsSuccess ? Ok(Model) : BadRequest();
    }

    [HttpGet("WithNoPosition")]
    [Role("Admin")]
    public async Task<IActionResult> GetUserWithoutPosition()
    {
        List<UserDTO> dtos = await _positionService.GetUserWithoutPosition();
        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    [Role("Admin")]
    public async Task<IActionResult> Delete(long id)
    {
        bool success = await _userService.Delete(id);
        return success ? Ok() : BadRequest();
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

    [HttpPut("Admin/{Id}")]
    [Role("Admin")]
    public async Task<IActionResult> ReserUserPassword(long Id, string NewPassword)
    {
        bool IsSuccess = await _userService.ResertUserPassword(Id, NewPassword);
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpPut("User/{Id}")]
    public async Task<IActionResult> ChangeUserPassword(long Id, string OldPassword, string NewPassword)
    {
        bool IsSuccess = await _userService.ChangeUSerPassword(Id, OldPassword, NewPassword);
        return IsSuccess ? Ok() : BadRequest();
    }


}