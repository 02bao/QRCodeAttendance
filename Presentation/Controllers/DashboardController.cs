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
        // trả về 1 list các object
        // mỗi object gồm 2 thuộc tính: DepartmentName và TotalEmployee
        // ví dụ : [{DepartmentName: "Phòng kế toán", TotalEmployee: 10}, {DepartmentName: "Phòng kỹ thuật", TotalEmployee: 5}]
        // lưu ý nếu nhiều hơn 6 department thì trả về 6 department có số lượng nhân viên nhiều nhất
        // vì sao lại 6, vì biểu đồ bên frontend chỉ hiển thị được 6 thôi haha hơi lỏ tí
    }
}
