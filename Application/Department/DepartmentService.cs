using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Department;

public class DepartmentService(
    DataContext _context) : IDepartmentService
{
    public async Task<bool> CreateNewDepartment(string Name, string Description)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Name == Name && s.IsDeleted == false).FirstOrDefaultAsync();
        if (department != null) { return false; }
        SqlDepartment NewDepartment = new()
        {
            Name = Name,
            Description = Description,
        };
        await _context.Departments.AddAsync(NewDepartment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(long Id)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Id == Id && s.IsDeleted == false).FirstOrDefaultAsync();
        if (department == null) { return false; }
        department.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<SqlDepartment>> GetAll()
    {
        List<SqlDepartment> deps = await _context.Departments.Where(s => s.IsDeleted == false).ToListAsync();
        return deps;
    }

    public async Task<SqlDepartment?> GetById(long Id)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Id == Id && s.IsDeleted == false).FirstOrDefaultAsync();
        if (department == null) { return null; }
        return department;
    }

    public async Task<bool> Update(long DepartmentId, DepartmentUpdate Departments)
    {
       SqlDepartment? department = await _context.Departments.Where(s => s.Id == DepartmentId)
                                                             .FirstOrDefaultAsync();
        if( department == null) { return false; }
        SqlDepartment? NewDepart = new SqlDepartment()
        {
            Name = Departments.Name,
            Description = Departments.Description,
            TotalEmployees = Departments.TotalEmployees,
            TotalPositions = Departments.TotalPositions,
        };
        _context.Departments.Add(NewDepart);
        await _context.SaveChangesAsync();
        return true;
    }
}
