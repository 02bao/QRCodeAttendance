using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Presentation.Models;

namespace QRCodeAttendance.Presentation.Controllers;

public class DepartmentController(IDepartmentService _departmentService) : BaseController
{
    [HttpPost("CreateNewDepartment")]
    public async Task<IActionResult> CreateNewDepartment(DepartmentCreateModel model)
    {
        bool IsSuccess = await _departmentService.CreateNewDepartment(model.Name, model.Description);
        return IsSuccess ? Ok(model) : BadRequest();
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        List<SqlDepartment> department = await _departmentService.GetAll();
        return Ok(department);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long Id)
    {
        SqlDepartment? department = await _departmentService.GetById(Id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(SqlDepartment Updates)
    {
        bool IsSuccess = await _departmentService.Update(Updates);
        return IsSuccess ? Ok(IsSuccess) : BadRequest();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _departmentService.DeleteById(Id);
        return IsSuccess ? Ok(IsSuccess) : BadRequest();
    }
}
