﻿using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Application.Cloud;
using QRCodeAttendance.Application.Token;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.User;

public class UserService(DataContext _context,
    ITokenService _tokenService) : IUserService
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

    public async Task<string> Create(string Email, string FullName,string Phone, string Password, bool IsWoman, long RoleId)
    {
        SqlUser? user = await _context.Users.Where(s => s.Email.CompareTo(Email) == 0).FirstOrDefaultAsync();
        if (user != null) { return ""; }
        SqlRole? role = await _context.Roles.Where(s => s.Id == RoleId).FirstOrDefaultAsync();
        if (role == null) { return ""; }
        SqlUser NewUser = new SqlUser
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
            .Where(s => s.VerifyToken == Token)
            .FirstOrDefaultAsync();
        if(user == null) { return false; }
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
            .ToListAsync();
        List<UserDTO> use = user.Select(s => s.ToDTO()).ToList();
        return use;
    }

    public async Task<UserDTO?> GetById(long Id)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .Include(s => s.Role)
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
            .FirstOrDefaultAsync();
        if (position == null) { return dtos; }
        dtos = position.Users.Select(s => s.ToDTO()).ToList();
        return dtos;
    }

    public async Task<bool> UploadImages(long UserId , List<IFormFile> Images)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return false; }
        if (Images != null && Images.Count > 0)
        {
            CloudinaryService _cloudinary = new CloudinaryService();
            string Url = _cloudinary.uploadFile(Images[0]);
            if (!string.IsNullOrEmpty(Url))
            {
                if (!string.IsNullOrEmpty(user.Images)) { _cloudinary.DeleteFile(user.Images); }
                user.Images = Url;
            }
        }
         _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Update(long UserId, string Email,string Phone, string FullName, bool IsWoman, long RoleId, string Images)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null) { return false;}
        if(!string.IsNullOrEmpty(Email))
        {
            bool ExistEmail = await _context.Users
                .Where(s => s.Email == Email && s.Id != UserId && s.IsDeleted == false)
                .AnyAsync();
            if(ExistEmail) { return false;}
            user.Email = Email;
        }
        if(!string.IsNullOrEmpty(Phone)) { user.Phone = Phone; }
        if(!string.IsNullOrEmpty(FullName)) { user.FullName = FullName; }
        if(!string.IsNullOrEmpty(Images)) { user.Images = Images; }
        user.IsWoman = IsWoman;
        user.RoleId = RoleId;
         _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeUSerPassword(long UserId,  string OldPassword, string NewPassword)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId &&
                        s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if(!string.IsNullOrEmpty(OldPassword) && user.Password != OldPassword) { return false;}
        if(string.IsNullOrEmpty(NewPassword)) { return false;}
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