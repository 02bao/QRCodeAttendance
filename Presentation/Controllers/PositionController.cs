using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Presentation.Controllers;

public class PositionController(
    IPositionService _positionService) : BaseController
{
    [HttpPost("CreateNewPosition")]
    public async Task<IActionResult> CreateNewPosition(long DepartmentId, PositionCreate Create)
    {
        bool IsSuccess = await _positionService.CreateNewPositions(DepartmentId,Create);
        return IsSuccess ? Ok(Create) : BadRequest();
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        List<SqlPosition> position = await _positionService.GetAll();
        return Ok(position);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long Id)
    {
        SqlPosition? position = await _positionService.GetById(Id);
        return Ok(position);
    }

    [HttpGet("GetByDepartmentId")]
    public async Task<IActionResult> GetByDepartmentId(long DepartmentId)
    {
        List<SqlPosition>? position = await _positionService.GetByDepartmentId(DepartmentId);
        return Ok(position);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(long PositionId, PositionUpdate Positions)
    {
        bool IsSuccess = await _positionService.Update(PositionId, Positions);
        return IsSuccess ? Ok(IsSuccess) : BadRequest();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _positionService.Delete(Id);
        return IsSuccess ? Ok(IsSuccess) : BadRequest();
    }
}
