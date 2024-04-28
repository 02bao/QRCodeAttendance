using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Presentation.Filters;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class UserController(IUserService userService) : BaseController
{

    private readonly IUserService _userService = userService;

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