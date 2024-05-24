using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Dashboard;

public class DashboardService(
    DataContext _context) : IDashboardService
{
    public async Task<DailyAttendanceStat> GetDailyAttendanceStat(DateTime date)
    {
        DateTime Startdate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
        DateTime EndDate = Startdate.AddDays(1);
        int OnTimeCount = await _context.Attendaces
            .Where(s => s.CreatedAt >= Startdate &&
                        s.CreatedAt < EndDate &&
                        s.Status == AttendaceStatus.OnTime)
            .CountAsync();
        int LateTimeCount = await _context.Attendaces
            .Where(s => s.CreatedAt >= Startdate &&
                        s.CreatedAt < EndDate &&
                        s.Status == AttendaceStatus.Late)
            .CountAsync();
        int AbsentCount = await _context.Attendaces
            .Where(s => s.CreatedAt >= Startdate &&
                        s.CreatedAt < EndDate &&
                        s.Status == AttendaceStatus.Absent)
            .CountAsync();
        return new DailyAttendanceStat
        {
            OnTimeCount = OnTimeCount,
            LateCount = LateTimeCount,
            AbsentCount = AbsentCount,
            StartDate = Startdate,
        };

    }

    public async Task<List<StatDepartment>> GetStatisticDepartment()
    {
        List<StatDepartment> Department = await _context.Departments
            .Where(s => s.IsDeleted == false)
            .Include(s => s.User)
            .Select(s => new StatDepartment
            {
                Department = s.Name,
                TotalEmployee = s.User.Count,
                TotalAttendance = s.User.Sum(u => u.Attendances.Count)
            })
            .OrderByDescending(s => s.TotalEmployee)
            .Take(6)
            .ToListAsync();
        return Department;
    }

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
