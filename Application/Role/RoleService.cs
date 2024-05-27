using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Role;

public class RoleService(
    DataContext _context) : IRoleService
{
    public async Task<bool> ChangeRoleForUser(long UserId, long RoleId)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId &&
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return false; }
        bool ExistRole = user.RoleId == RoleId;
        if(ExistRole) { return false; }
        user.RoleId = RoleId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CreateNewRole( string Name)
    {
        SqlRole? role = await _context.Roles
            .Where(s => s.Name == Name)
            .FirstOrDefaultAsync();
        if(role != null) { return false; }
        SqlRole NewRole = new()
        {
            Name = Name,
        };
        await _context.Roles.AddAsync(NewRole);
        await _context.SaveChangesAsync();  
        return true;
    }

    public async Task<bool> Delete(long Id)
    {
        SqlRole? role = await _context.Roles
            .Where(s => s.Id == Id && 
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if(role == null) { return false;}
        role.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<RoleDTO>> GetAll()
    {
        List<SqlRole>? role = await _context.Roles
            .Where(s => s.IsDeleted == false)
            .ToListAsync();
        if(role == null) { return null; }
        List<RoleDTO>? dto = role.Select(s => s.ToDto()).ToList();
        return dto;
    }

    public async Task<RoleDTO> GetById(long Id)
    {
        SqlRole? role = await _context.Roles
           .Where(s => s.Id == Id &&
                       s.IsDeleted == false)
           .FirstOrDefaultAsync();
        if (role == null) { return null; }
        RoleDTO? dto = role.ToDto();
        return dto;
    }

    public async Task<bool> Update(long Id, string Name)
    {
        SqlRole? role = await _context.Roles
            .Where(s => s.Id == Id)
            .FirstOrDefaultAsync();
        if (role == null) { return false;}
        bool ExistName = await _context.Roles
            .Where(s => s.Name == Name &&
                        s.Id != Id)
            .AnyAsync();
        if(ExistName) { return false; }
        role.Name = Name;
        await _context.SaveChangesAsync();
        return true;
    }
}
