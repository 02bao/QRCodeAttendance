using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Presentation.Filters;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class UserController(IUserService _userService) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserModel model)
    {
        UserAuthenticate response = await _userService.Login(model.Email, model.Password);

        if (string.IsNullOrEmpty(response.Email))
        {
            return BadRequest();
        }

        return Ok(response);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(string AdminToken, UserModelRegister Model)
    {
        bool IsSuccess = await _userService.Register(AdminToken,Model.Email, Model.FullName, Model.Password, Model.IsWoman, Model.RoleId);
        return IsSuccess? Ok(Model) : BadRequest();
    }

    [HttpGet("Test-authorization/{number}")]
    [Role("Admin")]

    public IActionResult Test(long number)
    {
        return Ok(number);
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
}