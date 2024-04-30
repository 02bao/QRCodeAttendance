using QRCodeAttendance.Application.Role;
using QRCodeAttendance.Application.Token;

namespace QRCodeAttendance.Application.Auth;

public class AuthDTO
{
}
public class UserAuthenticate
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public RoleDTO Role { get; set; } = new RoleDTO();
    public TokenItem Token { get; set; } = new TokenItem();
}