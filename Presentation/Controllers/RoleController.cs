using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Company;
using QRCodeAttendance.Application.Notification;
using QRCodeAttendance.Application.Role;
using QRCodeAttendance.Presentation.Filters;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class RoleController(
    IRoleService _roleService) : BaseController
{
    [HttpPost("")]
    [Role("Admin")]
    public async Task<IActionResult> CreateNewrole(string Name)
    {
        bool IsSuccess = await _roleService.CreateNewRole(Name);
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpPut("ChangeRoleForUser")]
    [Role("Admin")]
    public async Task<IActionResult> ChangeRoleForUser(long Id,long RoleId)
    {
        bool IsSuccess = await _roleService.ChangeRoleForUser(Id, RoleId);
        return IsSuccess ? Ok() : BadRequest();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        List<RoleDTO> dtos = await _roleService.GetAll();
        return Ok(dtos);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult>? GetById(long Id)
    {
        RoleDTO? dto = await _roleService.GetById(Id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(long Id, RoleUpdateModel Model)
    {
        bool IsSuccess = await _roleService.Update(Id,Model.Name);
        return IsSuccess ? Ok(Model) : BadRequest();
    }

    [HttpDelete("{id}")]
    [Role("Admin")]
    public async Task<IActionResult> Delete(long id)
    {
        bool success = await _roleService.Delete(id);
        return success ? Ok() : BadRequest();
    }

}
