using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Attendace;

public class AttendaceService(
    DataContext _context) : IAttendaceService
{
    public async Task<bool> CheckIn(long UserId,  long CompanyId)
    {
        DateTime CurrentDateTimeUtc = DateTime.Now;
        TimeSpan CurrentTime = new TimeSpan(CurrentDateTimeUtc.Hour, CurrentDateTimeUtc.Minute, CurrentDateTimeUtc.Second);
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return false; }
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
            Company = company,
            CreatedAt = DateTime.UtcNow.Date,
            Status = status,
        };
        _context.Attendaces.Add(NewAttendance);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<AttendaceDTO>> GetAll()
    {
        List<SqlAttendace>? attendace = await _context.Attendaces
            .ToListAsync();
        List<AttendaceDTO> dto = attendace.Select(s => s.ToDTO()).ToList();
        return dto;
    }

    public async Task<AttendanceGetByUser> GetByUserId(long USerId)
    {
        AttendanceGetByUser response = new();
        SqlUser? user = await _context.Users
            .Where(s => s.Id == USerId &&
                        s.IsDeleted == false)
            .Include(s => s.Attendances)
            .FirstOrDefaultAsync();
        if(user == null) { return response; }
        List<AttendaceDTO>? dto = user.Attendances.Select(s => s.ToDTO()).ToList();
        response.UserName = user.FullName;
        response.Attendaces = dto;
        return response;
    }
}
