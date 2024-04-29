using QRCodeAttendance.Application.Token;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.User;

public class UserDTO
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public long RoleId { get; set; }
    public bool IsWoman { get; set; } = false;
}
public class UserAuthenticate
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
    public TokenItem Token { get; set; } = new TokenItem();
}