using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class AuthController(IUserService _userService) : BaseController
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
}