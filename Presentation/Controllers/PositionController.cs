using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class PositionController(
    IPositionService _positionService) : BaseController
{
    [HttpPost("")]
    public async Task<IActionResult> CreateNewPosition(long DepartmentId, PositionCreateModel model)
    {
        bool IsSuccess = await _positionService.CreateNewPositions(DepartmentId, model.Name, model.Description);
        return IsSuccess ? Ok(model) : BadRequest();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        List<PositionDTO> dtos = await _positionService.GetAll();
        return Ok(dtos);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(long Id)
    {
        PositionDTO? dto = await _positionService.GetById(Id);
        if (dto == null)
        {
            return NotFound();
        }
        return Ok(dto);
    }

    [HttpGet("DepartmentId")]
    public async Task<IActionResult> GetByDepartmentId(long DepartmentId)
    {
        List<PositionDTO> dtos = await _positionService.GetByDepartmentId(DepartmentId);
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

    [HttpPost("UserId")]
    public async Task<IActionResult> AssignUserToPosition(long PositionId, long UserId)
    {
        bool IsSuccess = await _positionService.AssignUserToPosition(PositionId, UserId);
        return IsSuccess ? Ok(UserId) : BadRequest();
    }

    [HttpDelete("UserId")]
    public async Task<IActionResult> RemoveUserFromPosition(long PositionId, long UserId)
    {
        bool IsSuccess = await _positionService.RemoveUserFromPosition(PositionId, UserId);
        return IsSuccess ? Ok(UserId) : BadRequest();
    }
}
