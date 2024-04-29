using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class PositionsController(
    IPositionService _positionService) : BaseController
{
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