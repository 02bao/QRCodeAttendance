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
            Password = entity.Password,
            FullName = entity.FullName,
            IsWoman = entity.IsWoman,
            RoleName = entity.Role.Name,
        };
    }
}
