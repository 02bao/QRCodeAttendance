﻿using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Department;

public class DepartmentService(
    DataContext _context) : IDepartmentService
{
    public async Task<bool> CreateNewDepartment(string Name, string Description)
    {
        SqlDepartment? department = await _context.Departments
            .Where(s => s.Name == Name && s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (department != null) { return false; }
        SqlDepartment NewDepartment = new()
        {
            Name = Name,
            Description = Description,
        };
        await _context.Departments.AddAsync(NewDepartment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(long Id)
    {
        SqlDepartment? department = await _context.Departments
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .Include(s => s.Positions).ThenInclude(s => s.Users)
            .FirstOrDefaultAsync();

        if (department == null) { return false; }
        foreach (SqlPosition position in department.Positions)
        {
            position.Department = null;
            foreach (SqlUser user in position.Users)
            {
                user.Position = null;
                user.Department = null;
            }
        }
        department.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<DepartmentItemDTO>> GetAll()
    {
        List<SqlDepartment> deps = await _context.Departments
            .Where(s => s.IsDeleted == false)
            .Include(s => s.Positions)
            .Include(s => s.User)
            .ToListAsync();

        List<DepartmentItemDTO> dtos = deps.Select(s => s.ToDTO()).ToList();
        return dtos;
    }

    public async Task<DepartmentItemDTO?> GetById(long Id)
    {
        SqlDepartment? department = await _context.Departments
            .Where(s => s.Id == Id && s.IsDeleted == false)
            .Include(s => s.Positions)
            .Include(s => s.User)
            .FirstOrDefaultAsync();
        if (department == null) { return null; }
        DepartmentItemDTO dto = department.ToDTO();
        return dto;
    }


    public async Task<bool> Update(long DepartmentId, string? Name, string? Description)
    {
        SqlDepartment? department = await _context.Departments
            .Where(s => s.Id == DepartmentId && s.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (department == null) { return false; }

        if (!string.IsNullOrEmpty(Name))
        {
            bool ExistName = await _context.Departments
                .Where(s => s.Name == Name && s.Id != DepartmentId && s.IsDeleted == false)
                .AnyAsync();

            if (ExistName) { return false; }

            department.Name = Name;
        }

        if (!string.IsNullOrEmpty(Description))
        {
            department.Description = Description;
        }

        await _context.SaveChangesAsync();
        return true;
    }
}
