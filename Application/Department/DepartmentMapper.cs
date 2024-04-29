﻿using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Department;

public static class DepartmentMapper
{
    public static DepartmentItemDTO ToDTO(this SqlDepartment entity)
    {
        return new DepartmentItemDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            PositionCount = entity.Position.Where(s => s.IsDeleted == false).Count(),
        };
    }
}