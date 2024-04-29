using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Dashboard;

public class DashboardService(
    DataContext _context) : IDashboardService
{
    public async Task<StatEmployee> GetStatisticEmployee()
    {
        List<SqlUser> totalEmployee = await _context.Users.Where(s => s.IsDeleted == false).ToListAsync();

        int TotalEmployee = totalEmployee.Count();
        int WomanCount = totalEmployee.Where(s => s.IsWoman == true).Count();

        StatEmployee dto = new()
        {
            TotalEmployee = TotalEmployee,
            WomanCount = WomanCount
        };

        return dto;
    }
}
