using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class DepartmentController(IDepartmentService _departmentService) : BaseController
{
    [HttpPost("")]
    public async Task<IActionResult> CreateNewDepartment(DepartmentCreateModel model)
    {
        bool IsSuccess = await _departmentService.CreateNewDepartment(model.Name, model.Description);
        return IsSuccess ? Ok(model) : BadRequest();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        List<DepartmentItemDTO> dtos = await _departmentService.GetAll();
        return Ok(dtos);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(long Id)
    {
        DepartmentItemDTO? dto = await _departmentService.GetById(Id);
        if (dto == null)
        {
            return NotFound();
        }
        return Ok(dto);
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
}
