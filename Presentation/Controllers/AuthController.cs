﻿using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Auth;
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
}