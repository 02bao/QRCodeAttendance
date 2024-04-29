﻿using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Position;

public class PositionService(
    DataContext _context) : IPositionService
{
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
            .FirstOrDefaultAsync();

        if (position == null) { return false; }

        position.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<PositionDTO>> GetAll()
    {
        List<SqlPosition> position = await _context.Positions
            .Where(s => s.IsDeleted == false)
            .Include(s => s.Users)
            .ToListAsync();

        List<PositionDTO> pos = position.Select(s => s.ToDTO()).ToList();
        return pos;
    }

    public async Task<List<PositionDTO>> GetPositionsByDepartmentId(long DepartmentId)
    {
        List<SqlPosition>? positions = await _context.Positions
            .Where(s => s.Department.Id == DepartmentId && s.IsDeleted == false)
            .Include(s => s.Users)
            .Include(s => s.Department)
            .ToListAsync();

        List<PositionDTO> dtos = [];

        if (positions == null || positions.Count == 0) { return dtos; }

        dtos = positions.Select(s => s.ToDTO()).ToList();

        return dtos;
    }

    public async Task<PositionDTO?> GetById(long Id)
    {
        SqlPosition? position = await _context.Positions
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .Include(s => s.Users)
            .FirstOrDefaultAsync();

        if (position == null) { return null; }

        PositionDTO dto = position.ToDTO();
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

        if (!string.IsNullOrEmpty(Name)) { position.Name = Name; }

        if (!string.IsNullOrEmpty(Description)) { position.Description = Description; }

        await _context.SaveChangesAsync();
        return true;
    }
}