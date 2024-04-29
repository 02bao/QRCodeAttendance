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
        bool IsSuccess = await _positionService.CreateNewPositions(DepartmentId, model.PositionName, model.Description);
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

    [HttpGet("Department")]
    public async Task<IActionResult> GetByDepartmentId(long DepartmentId)
    {
        List<PositionDTO?> dtos = await _positionService.GetByDepartmentId(DepartmentId);
        return Ok(dtos);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(long PositionId, PositionUpdateModel model)
    {
        bool IsSuccess = await _positionService.Update(PositionId, model.PositionName, model.Description);
        return IsSuccess ? Ok(PositionId) : BadRequest();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _positionService.Delete(Id);
        return IsSuccess ? Ok(Id) : BadRequest();
    }
}
