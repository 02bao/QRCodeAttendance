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
}
