using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;
using System.Globalization;

namespace QRCodeAttendance.Application.Company;

public class CompanyService(
    DataContext _context) : ICompanyService
{
    public async Task<bool> CreateNew(string Name, string Email, string StartTime, string MaxLateTime)
    {
        SqlCompany? company = await _context.Companies
            .Where(s => s.Name == Name || s.Email == Email)
            .FirstOrDefaultAsync();
        if(company != null) { return false; }
        TimeSpan startTimeSpan, maxLateTimeSpan;
        var startTime = TimeSpan.Parse(StartTime);
        var maxLateTime = TimeSpan.Parse(MaxLateTime);
        SqlCompany NewCompany = new()
        {
            Name = Name,
            Email = Email,
            StartTime = startTime,
            MaxLateTime = maxLateTime
        };
        await _context.Companies.AddAsync(NewCompany);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(long Id)
    {

        SqlCompany? company = await _context.Companies
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if(company == null) { return false; }
        company.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CompanyDTO>> GetAll()
    {
        List<SqlCompany>? company = await _context.Companies
            .Where(s => s.IsDeleted == false)
            .ToListAsync();
        List<CompanyDTO> dto = company.Select(s => s.ToDTO()).ToList();
        return dto;
    }

    public async Task<CompanyDTO>? GetById(long Id)
    {
        SqlCompany? company = await _context.Companies
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if(company == null) { return null; }
        CompanyDTO dto = company.ToDTO();
        return dto;
    }

    public async Task<bool> Update(long CompanyId, string Name,string Email, string StartTime, string MaxLateTime, long FileId)
    {
        SqlCompany? company = await _context.Companies
            .Where(s => s.Id == CompanyId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if(company == null) { return false;}
        if(!string.IsNullOrEmpty(Name))
        {
            bool ExistName = await _context.Companies
                .Where(s => s.Name == Name && s.Id != CompanyId && s.IsDeleted == false)
                .AnyAsync();
            if(ExistName) { return false; }
            company.Name = Name;
        }
        if (!string.IsNullOrEmpty(Email))
        {
            bool ExistName = await _context.Companies
                .Where(s => s.Email == Name && s.Id != CompanyId && s.IsDeleted == false)
                .AnyAsync();
            if (ExistName) { return false; }
            company.Email = Email;
        }
        TimeSpan startTimeSpan, maxLateTimeSpan;
        var startTime = TimeSpan.Parse(StartTime);
        var maxLateTime = TimeSpan.Parse(MaxLateTime);
        company.StartTime = startTime;
        company.MaxLateTime = maxLateTime;
        if (FileId > 0)
        {
            SqlFile? file = await _context.Files.FindAsync(FileId);
            if (file != null) { company.Images = file; }
        }
        await _context.SaveChangesAsync();
        return true;
    }
}
