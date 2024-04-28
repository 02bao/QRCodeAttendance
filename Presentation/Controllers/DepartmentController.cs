using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Domain.Entities;
using System.Data.SqlTypes;

namespace QRCodeAttendance.Presentation.Controllers;

public class DepartmentController(IDepartmentService _departmentService) : BaseController
{
    [HttpPost("CreateNewDepartment")]
    public async Task<IActionResult> CreateNewDepartment(DepartmentCreate Create)
    {
        bool IsSuccess =await _departmentService.CreateNewDepartment(Create);
        return IsSuccess ? Ok(Create) : BadRequest();
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
        return Ok(department);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(long DepartmentId, DepartmentUpdate Departments)
    {
        bool IsSuccess = await _departmentService.Update(DepartmentId, Departments);
        return IsSuccess ? Ok(IsSuccess) : BadRequest();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(long Id)
    {
        bool IsSuccess = await _departmentService.DeleteById(Id);
        return IsSuccess ? Ok(IsSuccess) : BadRequest();
    }
}
