using QRCodeAttendance.Application.Attendace;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Application.Role;

namespace QRCodeAttendance.Application.User;

public class UserDTO
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsWoman { get; set; } = false;
    public string Images { get; set; } = "";
    public RoleDTO Role { get; set; } = null!;
    public PositionDto? Position { get; set; } = null;
}

//public class UserGetAttendance
//{
//    public string FullName { get; set; } = "";
//    public bool IsWoman { get; set; } = false;
//    public List<AttendanceGetByUser> Attendance { get; set; } = new List<AttendanceGetByUser>();
//}


