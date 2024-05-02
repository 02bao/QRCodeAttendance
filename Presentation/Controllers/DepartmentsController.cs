using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class DepartmentsController(
    IDepartmentService _departmentService,
    IPositionService _positionService) : BaseController
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        List<DepartmentItemDTO> dtos = await _departmentService.GetAll();
        return Ok(dtos);
    }


    [HttpPost("")]
    public async Task<IActionResult> CreateDepartment(DepartmentCreateModel model)
    {
        bool IsSuccess = await _departmentService.CreateNewDepartment(model.Name, model.Description);
        return IsSuccess ? Ok(model) : BadRequest();
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(long Id)
    {
        DepartmentItemDTO? dto = await _departmentService.GetById(Id);
        return dto == null ? NotFound() : Ok(dto);
    }


    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(long Id, DepartmentUpdateModel model)
    {
        bool IsSuccess = await _departmentService.Update(Id, model.Name, model.Description);
        return IsSuccess ? Ok(Id) : BadRequest();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _departmentService.DeleteById(Id);
        return IsSuccess ? Ok(Id) : BadRequest();
    }

    [HttpGet("{Id}/positions")]
    public async Task<IActionResult> GetPositionsByDepartmentId(long Id)
    {
        GetPositionsByDepartmentIdDTO dtos = await _positionService.GetPositionsByDepartmentId(Id);
        return Ok(dtos);
    }

    [HttpPost("{Id}/positions")]
    public async Task<IActionResult> CreatePosition(long Id, PositionCreateModel model)
    {
        bool IsSuccess = await _positionService.CreateNewPositions(Id, model.Name, model.Description);
        return IsSuccess ? Ok() : BadRequest();
    }
}