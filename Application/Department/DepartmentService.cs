using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Department;

public class DepartmentService(
    DataContext _context) : IDepartmentService
{
    public async Task<bool> CreateNewDepartment(DepartmentCreate Create)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Name == Create.Name).FirstOrDefaultAsync();
        if(department != null) { return false; }
        SqlDepartment NewDepartment = new SqlDepartment()
        {
            Name = Create.Name,
            Description = Create.Description,
        };
        _context.Departments.Add(NewDepartment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(long Id)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Id == Id).FirstOrDefaultAsync();
        if(department == null) { return false; }
         _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<SqlDepartment>> GetAll()
    {
       List<SqlDepartment>? NewDepart = await _context.Departments.ToListAsync();
        return NewDepart;
    }

    public async Task<SqlDepartment> GetById(long Id)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Id == Id).FirstOrDefaultAsync();
        if(department == null) { return null; }
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
