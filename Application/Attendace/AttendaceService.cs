using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Attendace;

public class AttendaceService(
    DataContext _context) : IAttendaceService
{
    public async Task<bool> CheckIn(long UserId, long DepartmentId, long CompanyId)
    {
        DateTime CurrentDateTimeUtc = DateTime.Now;
        TimeSpan CurrentTime = new TimeSpan(CurrentDateTimeUtc.Hour, CurrentDateTimeUtc.Minute, CurrentDateTimeUtc.Second);
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
        TimeSpan CompanyStartTime = company.StartTime;
        TimeSpan MaxLateTime = company.MaxLateTime;
        var status = AttendaceStatus.OnTime;
        bool Present = true;
        if (CurrentTime > CompanyStartTime.Add(MaxLateTime))
        {
            status = AttendaceStatus.Absent;
            Present = false;
        }
        else if (CurrentTime > CompanyStartTime)
        {
            status = AttendaceStatus.Late;
        }

        SqlAttendace NewAttendance = new()
        {
            CheckInTime = CurrentTime,
            IsPresent = Present,
            User = user,
            Department = department,
            Company = company,
            CreatedAt = DateTime.UtcNow,
            Status = status,
        };
        _context.Attendaces.Add(NewAttendance);
        await _context.SaveChangesAsync();
        return true;
    }
}
