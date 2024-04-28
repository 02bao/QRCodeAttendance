using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Position;

public class PositionService(
    DataContext _context) : IPositionService
{
    public async Task<bool> CreateNewPositions(long DepartmentId, PositionCreate Create)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Id == DepartmentId)
                                                              .FirstOrDefaultAsync();
        if(department == null) { return false; }
        SqlPosition NewPosition = new SqlPosition()
        {
            Department = department,
            PositionName = Create.PositionName,
            Description = Create.Description,
        };
        _context.Positions.Add(NewPosition);
        department.TotalPositions += 1;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(long Id)
    {
        SqlPosition? position = await _context.Positions.Include(s => s.Department)
                                                        .Where(s => s.Id == Id)
                                                        .FirstOrDefaultAsync();
        if(position == null) { return false; }
        _context.Positions.Remove(position);
        position.Department.TotalPositions -= 1;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<SqlPosition>> GetAll()
    {
        return await _context.Positions.ToListAsync();
    }

    public async Task<List<SqlPosition>> GetByDepartmentId(long DepartmentId)
    {
        List<SqlPosition>? NewPosi = new List<SqlPosition>();
        List<SqlPosition>? position = await _context.Positions.Where(s => s.Department.Id == DepartmentId).ToListAsync();
        if(position == null) { return NewPosi; }
        foreach(var newposi in  position)
        {
            NewPosi.Add(new SqlPosition()
            {
                Id = newposi.Id,
                Department = newposi.Department,
                PositionName = newposi.PositionName,
                Description = newposi.Description,
                EmployeeCount = newposi.EmployeeCount,
            });
        }
        return NewPosi;
    }

    public async Task<SqlPosition> GetById(long Id)
    {
        SqlPosition? NewPosi = new SqlPosition();
        SqlPosition? position =  await _context.Positions.Where(s => s.Id == Id).FirstOrDefaultAsync();
        if (position == null) { return NewPosi; }
        return position;
    }

    public async Task<bool> Update(long PositionId, PositionUpdate Positions)
    {
        SqlPosition? position = await _context.Positions.Include(s => s.Department)
                                                        .Where(s => s.Id == PositionId)
                                                        .FirstOrDefaultAsync();  
        if(position == null) { return false; }
        SqlPosition NewPosi = new SqlPosition()
        {
            Department = position.Department,
            PositionName = Positions.PositionName,
            Description = Positions.Description,
            EmployeeCount = Positions.EmployeeCount,
        };
        _context.Positions.Add(NewPosi);
        await _context.SaveChangesAsync();
        return true;
    }
}
