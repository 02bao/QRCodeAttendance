using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Application.Notification;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;
using System.Globalization;

namespace QRCodeAttendance.Application.Attendace;

public class AttendaceService(
    DataContext _context,
    INotificationService _notificationService) : IAttendaceService
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
        await _notificationService.NotifyCheckIn(user, CurrentDateTimeUtc);
        return true;
    }


    public async Task<AttendanceGetByUser> GetByUserId(long UserId, DateTime date)
    {

        DateTime Startdate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
        DateTime EndDate = Startdate.AddDays(1);
        AttendanceGetByUser response = new();
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId &&
                        s.IsDeleted == false)
            .Include(s => s.Attendances)
            .FirstOrDefaultAsync();
        if(user == null) { return response; }
        List<AttendaceDTO>? dto = user.Attendances
            .Where(s => s.CreatedAt >= Startdate && 
            s.CreatedAt <= EndDate)
            .Select(s => s.ToDTO()).ToList();
        response.UserName = user.FullName;
        response.Attendaces = dto;
        return response;
    }

    public async Task<AttendanceGetByUserInMonth> GetByUserInMonth(long UserId, string month)
    {
        AttendanceGetByUserInMonth response = new();
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId &&
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return response; }

        if (!int.TryParse(month, out int inputMonth) || inputMonth < 1 || inputMonth > 12)
        {
            return response;
        }

        int year = DateTime.UtcNow.Year; 
        int daysInMonth = DateTime.DaysInMonth(year, inputMonth);

        DateTime monthStartDate = DateTime.SpecifyKind(new DateTime(year, inputMonth, 1), DateTimeKind.Utc); 
        DateTime monthEndDate = DateTime.SpecifyKind(new DateTime(year, inputMonth, daysInMonth, 23, 59, 59), DateTimeKind.Utc); 

        int onTimeCount = 0;
        int lateCount = 0;
        int absentCount = 0;
        for (int i = 0; i < daysInMonth; i++)
        {
            DateTime currentDate = monthStartDate.AddDays(i);
            DateTime nextDate = monthStartDate.AddDays(i + 1);
            int dailyOnTimeCount = await _context.Attendaces
                .CountAsync(a => a.User.Id == UserId && a.CreatedAt >= currentDate && a.CreatedAt < nextDate && a.Status == AttendaceStatus.OnTime);

            if (dailyOnTimeCount > 0)
            {
                onTimeCount++;
                continue;
            }

            int dailyLateCount = await _context.Attendaces
                .CountAsync(a => a.User.Id == UserId && a.CreatedAt >= currentDate && a.CreatedAt < nextDate && a.Status == AttendaceStatus.Late);

            if (dailyLateCount > 0)
            {
                lateCount++;
                continue;
            }

            int dailyAbsentCount = await _context.Attendaces
                .CountAsync(a => a.User.Id == UserId && a.CreatedAt >= currentDate && a.CreatedAt < nextDate && a.Status == AttendaceStatus.Absent);

            if (dailyAbsentCount > 0)
            {
                absentCount++;
            }
        }
        response.UserName = user.FullName;
        response.OnTimeCount = onTimeCount;
        response.LateCount = lateCount;
        response.AbsentCount = absentCount;
        response.StartDate = monthStartDate;
        response.EndDate = monthEndDate;

        return response;
    }

}
