using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Attendace;

public class AttendaceService(
    DataContext _context) : IAttendaceService
{
    public async Task<bool> CheckIn(long UserId, long DepartmentId, long CompanyId)
    {
        TimeSpan today = DateTime.Now.TimeOfDay;
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return false; }
        var department = await _context.Departments
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.Id == DepartmentId);
        if (department == null) { return false; }
        var company = await _context.Companies
            .Where(s => s.Id == CompanyId)
            .FirstOrDefaultAsync();
        if (company == null) { return false; }
        TimeSpan maxLateTime = company.MaxLateTime;
        var status = AttendaceStatus.OnTime;
        bool Present = true;

        if (today > company.StartTime.Add(maxLateTime))
        {
            status = AttendaceStatus.Absent;
            Present = false;
        }
        else if (today > company.StartTime)
        {
            status = AttendaceStatus.Late;
        }

        SqlAttendace NewAttendance = new()
        {
            CheckInTime = today,
            IsPresent = Present,
            User = user,
            Department = department,
            Company = company,
            CreatedAt = DateTime.Now,
            Status = status,
        };
        _context.Attendaces.Add(NewAttendance);
        await _context.SaveChangesAsync();
        return true;
    }
}
