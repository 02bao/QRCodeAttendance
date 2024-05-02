using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.Dashboard;

namespace QRCodeAttendance.Presentation.Controllers;

public class DashboardController(IDashboardService _dashboardService) : BaseController
{
    [HttpGet("stat-employee")]
    public async Task<IActionResult> GetStatisticEmployee()
    {
        StatEmployee dto = await _dashboardService.GetStatisticEmployee();
        return Ok(dto);
    }

    [HttpGet("stat-department")]
    public async Task<IActionResult> GetStatisticDepartment()
    {
        List<StatDepartment> dto = await _dashboardService.GetStatisticDepartment();
        return Ok(dto);
    }
}
