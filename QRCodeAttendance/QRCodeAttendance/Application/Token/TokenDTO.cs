namespace QRCodeAttendance.QRCodeAttendance.Application.Token;
internal class TokenDTO
{
}
public class TokenItem
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}
public class TokenDecodedDTO
{
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}
