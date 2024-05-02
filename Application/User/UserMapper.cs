using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Application.Role;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.User;

public static class UserMapper
{
    public static UserDTO ToDTO(this SqlUser entity)
    {
        return new UserDTO
        {
            Id = entity.Id,
            Email = entity.Email,
            FullName = entity.FullName,
            Phone = entity.Phone,
            IsWoman = entity.IsWoman,
            Images = entity.Images?.Path ?? "",
            Role = entity.Role.ToDto(),
            Position = entity.Position?.ToPositionDto()
        };
    }
}
