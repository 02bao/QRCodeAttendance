﻿using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Application.Role;
using QRCodeAttendance.Application.Token;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;
using Serilog;

namespace QRCodeAttendance.Application.Auth;

public class AuthService(DataContext _context,
    ITokenService _tokenService) : IAuthService
{
    public async Task<bool> ChangePassword(long UserId, string OldPwd, string NewPwd)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (user == null || string.IsNullOrEmpty(OldPwd) || user.Password != OldPwd)
        {
            return false;
        }

        user.Password = NewPwd;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserAuthenticate> Login(string Email, string Password)
    {
        try
        {
            SqlUser? user = _context.Users
                .Where(s => s.Email.CompareTo(Email) == 0 &&
                            s.Password.CompareTo(Password) == 0 &&
                            s.IsDeleted == false &&
                            s.IsVerified == true)
                .Include(s => s.Role)
                .FirstOrDefault();

            if (user == null)
            {
                return new UserAuthenticate();
            }

            TokenItem token = await _tokenService.GenerateToken(user, user.Role.Name);

            if (string.IsNullOrEmpty(token.AccessToken)
                || string.IsNullOrEmpty(token.RefreshToken))
            {
                return new UserAuthenticate();
            }

            UserAuthenticate item = new()
            {
                Id = user.Id,
                Email = Email,
                Role = user.Role.ToDto(),
                Token = token
            };

            return item;
        }
        catch (Exception ex)
        {
            Log.Error($"func: login - failed with username: {Email} and password: {Password} - Exception : {ex.InnerException}");
            return new UserAuthenticate();
        }
    }

    public async Task<bool> ResetPassword(long UserId, string NewPwd)
    {
        SqlUser? user = await _context.Users
            .Where(s => s.Id == UserId && s.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            return false;
        }
        user.Password = NewPwd;
        await _context.SaveChangesAsync();
        return true;
    }
}
