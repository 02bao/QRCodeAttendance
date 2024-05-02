using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Position;

public static class PositionMapper
{
    public static PositionItemDTO ToDTO(this SqlPosition entity)
    {
        return new PositionItemDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            EmployeeCount = entity.Users.Where(s => s.IsDeleted == false).Count(),
        };
    }
    public static PositionDto ToPositionDto(this SqlPosition entity)
    {
        return new PositionDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
    }
}
