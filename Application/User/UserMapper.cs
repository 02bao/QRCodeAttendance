using QRCodeAttendance.Application.Attendace;
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

    //public static UserGetAttendance ToAttendanceDTO(this SqlUser entity)
    //{
    //    return new UserGetAttendance
    //    {
    //        FullName = entity.FullName,
    //        IsWoman = entity.IsWoman,
    //        Attendance = entity.Attendances.Select(s => s.ToUserDTO()).ToList(),
    //    };
    //}

}
