using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.User;

public class UserService(DataContext _context) : IUserService
{
    public async Task<bool> Delete(long id)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == id &&
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> Create(string Email, string FullName, string Phone, string Password, bool IsWoman, long RoleId)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Email.CompareTo(Email) == 0 && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user != null) { return ""; }
        SqlRole? role = await _context.Roles
            .Where(s => s.Id == RoleId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (role == null) { return ""; }
        SqlUser NewUser = new()
        {
            Email = Email,
            FullName = FullName,
            Phone = Phone,
            Password = Password,
            VerifyToken = _context.RandomString(20),
            IsWoman = IsWoman,
            Role = role
        };
        await _context.Users.AddAsync(NewUser);
        await _context.SaveChangesAsync();
        return NewUser.VerifyToken;
    }
    public async Task<bool> VerifyUser(string Token)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.VerifyToken == Token && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return false; }
        user.IsVerified = true;
        user.VerifyToken = string.Empty;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserDTO>> GetAll()
    {
        List<SqlUser> user = await _context.Users
            .Where(s => s.IsDeleted == false)
            .Include(s => s.Role)
            .Include(s => s.Position)
            .ToListAsync();
        List<UserDTO> dtos = user.Select(s => s.ToDTO()).ToList();
        return dtos;
    }

    public async Task<UserDTO?> GetById(long Id)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .Include(s => s.Role)
            .Include(s => s.Position)
            .FirstOrDefaultAsync();
        if (user == null) { return null; }
        UserDTO use = user.ToDTO();
        return use;
    }

    public async Task<List<UserDTO>> GetUsersByPositionId(long PositionId)
    {
        List<UserDTO> dtos = [];
        SqlPosition? position = await _context.Positions
            .Where(s => s.Id == PositionId)
            .Include(s => s.Users)
            .ThenInclude(s => s.Role)
            .Include(s => s.Users)
            .ThenInclude(s => s.Position)
            .FirstOrDefaultAsync();
        if (position == null) { return dtos; }
        dtos = position.Users.Select(s => s.ToDTO()).ToList();
        return dtos;
    }


    public async Task<bool> Update(long UserId, string Email, string Phone, string FullName, bool IsWoman, long RoleId, long FileId)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return false; }
        if (!string.IsNullOrEmpty(Email))
        {
            bool ExistEmail = await _context.Users
                .Where(s => s.Email == Email && s.Id != UserId && s.IsDeleted == false)
                .AnyAsync();
            if (ExistEmail) { return false; }
            user.Email = Email;
        }
        if (!string.IsNullOrEmpty(Phone)) { user.Phone = Phone; }
        if (!string.IsNullOrEmpty(FullName)) { user.FullName = FullName; }
        if (FileId > 0)
        {
            //user.Images = Images; 
            SqlFile? file = await _context.Files.FindAsync(FileId);
            if (file != null) { user.Images = file; }
        }
        user.IsWoman = IsWoman;
        user.RoleId = RoleId;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeUSerPassword(long UserId, string OldPassword, string NewPassword)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId &&
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (!string.IsNullOrEmpty(OldPassword) && user.Password != OldPassword) { return false; }
        if (string.IsNullOrEmpty(NewPassword)) { return false; }
        user.Password = NewPassword;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ResertUserPassword(long UserId, string NewPassword)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId &&
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (string.IsNullOrEmpty(NewPassword)) { return false; }
        user.Password = NewPassword;
        await _context.SaveChangesAsync();
        return true;
    }
}