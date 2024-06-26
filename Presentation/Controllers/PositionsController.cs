﻿using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class PositionsController(
    IPositionService _positionService,
    IUserService _userService) : BaseController
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        List<PositionItemDTO> dtos = await _positionService.GetAll();
        return Ok(dtos);
    }


    [HttpGet("WithNoDepartment")]
    public async Task<IActionResult> GetPositionWithoutDepartment()
    {
        List<PositionItemDTO> dtos = await _positionService.GetPositionWithoutDeparment();
        return Ok(dtos);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(long Id)
    {
        PositionItemDTO? dto = await _positionService.GetById(Id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpGet("{Id}/users")]
    public async Task<IActionResult> GetUsersByPositionId(long Id)
    {
        List<UserDTO> dtos = await _userService.GetUsersByPositionId(Id);
        return Ok(dtos);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(long Id, PositionUpdateModel model)
    {
        bool IsSuccess = await _positionService.Update(Id, model.Name, model.Description);
        return IsSuccess ? Ok(Id) : BadRequest();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _positionService.Delete(Id);
        return IsSuccess ? Ok(Id) : BadRequest();
    }
}