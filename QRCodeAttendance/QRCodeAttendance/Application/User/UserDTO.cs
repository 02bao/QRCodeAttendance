using QRCodeAttendance.Application.Token;

namespace QRCodeAttendance.Application.User;

public class UserDTO
{
}
public class UserAuthenticate
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
    public TokenItem Token { get; set; } = new TokenItem();
}