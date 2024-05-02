using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Auth;
using QRCodeAttendance.Presentation.Filters;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class AuthController(IAuthService _authService) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserModel model)
    {
        UserAuthenticate response = await _authService.Login(model.Email, model.Password);

        return string.IsNullOrEmpty(response.Email) ? BadRequest() : Ok(response);
    }

    [HttpPut("Admin/ResetPwd")]
    [Role("Admin")]
    public async Task<IActionResult> ResetUserPassword(ResetPwdModel model)
    {
        bool IsSuccess = await _authService.ResetPassword(model.UserId, model.NewPassword);
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpPut("ChangePwd")]
    public async Task<IActionResult> ChangePassword(ChangePwdModel model)
    {
        long Id = long.Parse(HttpContext.Items["Id"] as string ?? "0");

        bool IsSuccess = await _authService.ChangePassword(Id, model.OldPassword, model.NewPassword);
        return IsSuccess ? Ok() : BadRequest();
    }

}