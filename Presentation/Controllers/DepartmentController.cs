using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Department;

namespace QRCodeAttendance.Presentation.Controllers;

public class DepartmentController(IDepartmentService _departmentService) : BaseController
{
    [HttpGet("CreateNewDepartment")]
    public async Task<IActionResult> CreateNewDepartment(DepartmentCreate Create)
    {
        bool IsSuccess =await _departmentService.CreateNewDepartment(Create);
        return IsSuccess ? Ok(Create) : BadRequest();
    }
}
