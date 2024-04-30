using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Role;

public static class RoleMapper
{
    public static RoleDTO ToDto(this SqlRole role)
    {
        return new RoleDTO
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}
