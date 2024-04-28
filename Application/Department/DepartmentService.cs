using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Department;

public class DepartmentService(
    DataContext _context) : IDepartmentService
{
    public async Task<bool> CreateNewDepartment(DepartmentCreate Create)
    {
        SqlDepartment? department = _context.Departments.SingleOrDefault(s => s.Name == Create.Name);
        if(department != null) { return false; }
        SqlDepartment NewDepartment = new SqlDepartment()
        {
            Name = Create.Name,
            Description = Create.Description,
            TotalEmployees = Create.TotalEmployees,
            TotalPositions = Create.TotalPositions,
        };
        _context.Departments.Add(NewDepartment);
        _context.SaveChangesAsync();
        return true;
    }
}
