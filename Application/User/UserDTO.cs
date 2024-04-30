using QRCodeAttendance.Application.Role;

namespace QRCodeAttendance.Application.User;

public class UserDTO
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsWoman { get; set; } = false;
    public string? Images { get; set; }
    public RoleDTO Role { get; set; } = new RoleDTO();
}
