using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Position;

public class PositionService(
    DataContext _context) : IPositionService
{
    public async Task<List<PositionItemDTO>> GetPositionWithoutDeparment()
    {
        List<SqlPosition> position = await _context.Positions
            .Where(s => s.Department == null && s.IsDeleted == false)
            .Include(s => s.Users)
            .ToListAsync();
        List<PositionItemDTO> dtos = position.Select(s => s.ToDTO()).ToList();
        return dtos;
    }
    public async Task<bool> AssignUserToPosition(long UserId, long PositionId)
    {
        SqlPosition? position = await _context.Positions
            .Where(s => s.Id == PositionId && s.IsDeleted == false)
            .Include(s => s.Users)
            .FirstOrDefaultAsync();

        if (position == null) { return false; }

        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (user == null) { return false; }

        if (position.Users.Contains(user)) { return false; }

        position.Users.Add(user);
        position.Department.User.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CreateNewPositions(long DepartmentId, string Name, string Description)
    {
        SqlDepartment? department = await _context.Departments
            .Where(s => s.Id == DepartmentId && s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (department == null) { return false; }

        SqlPosition NewPosi = new()
        {
            Department = department,
            Name = Name,
            Description = Description
        };
        await _context.Positions.AddAsync(NewPosi);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(long Id)
    {
        SqlPosition? position = await _context.Positions
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .Include(s => s.Users)
            .FirstOrDefaultAsync();

        if (position == null) { return false; }
        foreach (SqlUser user in position.Users)
        {
            user.Position = null;
        }
        position.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<PositionItemDTO>> GetAll()
    {
        List<SqlPosition> position = await _context.Positions
            .Where(s => s.IsDeleted == false)
            .Include(s => s.Users)
            .ToListAsync();

        List<PositionItemDTO> pos = position.Select(s => s.ToDTO()).ToList();
        return pos;
    }

    public async Task<List<PositionItemDTO>> GetPositionsByDepartmentId(long DepartmentId)
    {
        SqlDepartment? department = await _context.Departments
            .Where(s => s.Id == DepartmentId && s.IsDeleted == false)
            .Include(s => s.Positions)
            .FirstOrDefaultAsync();

        if (department == null)
        {
            return [];
        }

        List<PositionItemDTO> dtos = department.Positions.Select(s => s.ToDTO()).ToList();
        return dtos;
    }

    public async Task<PositionItemDTO?> GetById(long Id)
    {
        SqlPosition? position = await _context.Positions
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .Include(s => s.Users)
            .FirstOrDefaultAsync();

        if (position == null) { return null; }

        PositionItemDTO dto = position.ToDTO();
        return dto;
    }

    public async Task<bool> RemoveUserFromPosition(long UserId, long PositionId)
    {
        SqlPosition? position = await _context.Positions
            .Where(s => s.Id == PositionId && s.IsDeleted == false)
            .Include(s => s.Users)
            .FirstOrDefaultAsync();

        if (position == null) { return false; }

        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (user == null) { return false; }

        if (!position.Users.Contains(user)) { return false; }

        position.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(long PositionId, string? Name, string? Description)
    {
        SqlPosition? position = await _context.Positions
            .Where(s => s.Id == PositionId && s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (position == null) { return false; }

        if (!string.IsNullOrEmpty(Name))
        {
            bool ExistName = await _context.Positions
                .Where(s => s.Name == Name && s.Id != PositionId && s.IsDeleted == false)
                .AnyAsync();
            if (!ExistName) { return false; }
            position.Name = Name;
        }

        if (!string.IsNullOrEmpty(Description)) { position.Description = Description; }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserDTO>> GetUserWithoutPosition()
    {
        List<SqlUser> user = await _context.Users
            .Where(s => s.Position == null &&
                        s.IsDeleted == false &&
                        s.IsVerified == true)
            .Include(s => s.Role)
            .ToListAsync();
        List<UserDTO> dtos = user.Select(s => s.ToDTO()).ToList();
        return dtos;
    }
}