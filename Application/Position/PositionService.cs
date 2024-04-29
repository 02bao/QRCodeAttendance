using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Position;

public class PositionService(
    DataContext _context) : IPositionService
{
    public async Task<bool> CreateNewPositions(long DepartmentId, string Name, string Description)
    {
        SqlDepartment? department = await _context.Departments.Where(s => s.Id == DepartmentId &&
                                                              s.IsDeleted == false)
                                                              .FirstOrDefaultAsync();  
        if(department == null) { return false; }
        SqlPosition NewPosi = new()
        {
            Department = department,
            PositionName = Name,
            Description = Description
        };
        await _context.Positions.AddAsync(NewPosi);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(long Id)
    {
        SqlPosition? position = await _context.Positions.Where(s => s.Id == Id &&
                                                         s.IsDeleted == false)
                                                        .FirstOrDefaultAsync();     
        if(position == null) { return false; }
        position.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<PositionDTO>> GetAll()
    {
        List<SqlPosition> position = await _context.Positions.Include(s => s.User)
                                                             .Where(s => s.IsDeleted == false)
                                                             .ToListAsync();
        List<PositionDTO> pos = position.Select(s => s.ToDTO()).ToList();
        return pos;
                
    }

    public async Task<List<PositionDTO>?> GetByDepartmentId(long DepartmentId)
    {
       
        List<SqlPosition>? positions = await _context.Positions.Include(s => s.Department)
                                                                  .Where(s => s.Department.Id == DepartmentId &&
                                                                  s.Department.IsDeleted == false &&
                                                                  s.IsDeleted == false)
                                                                  .Include(s => s.User)
                                                                  .ToListAsync();
        if(positions == null || positions.Count == 0) { return null; }
        List<PositionDTO>? NewPosi = positions.Select(s => s.ToDTO()).ToList();
        return NewPosi;
                                                                    
    }

    public async Task<PositionDTO?> GetById(long Id)
    {
        SqlPosition? position = await _context.Positions.Where(s => s.Id == Id &&
                                                       s.IsDeleted == false)
                                                       .Include(s => s.User)
                                                       .FirstOrDefaultAsync();
        if(position == null) { return null; }
        PositionDTO Pos = position.ToDTO();
        return Pos;
    }

    public async Task<bool> Update(long PositionId, string? Name, string? Description)
    {
        SqlPosition? position = await _context.Positions.Where(s => s.Id == PositionId &&
                                                         s.IsDeleted == false)
                                                        .FirstOrDefaultAsync();
        if(position == null) { return false;}
        if(!string.IsNullOrEmpty(Name)) { position.PositionName = Name; }
        if(!string.IsNullOrEmpty(Description)) {  position.Description = Description; }
        await _context.SaveChangesAsync();
        return true;
    }
}
