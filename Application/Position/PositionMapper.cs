﻿using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Position;

public static class PositionMapper
{
    public static PositionDTO ToDTO(this SqlPosition entity)
    {
        return new PositionDTO
        {
            Id = entity.Id,
            PositionName = entity.PositionName,
            Description = entity.Description,
            EmployeeCount = entity.User.Where(s => s.IsDeleted == false).Count(),
        };
    }
}
