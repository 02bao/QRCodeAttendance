namespace QRCodeAttendance.Presentation.Models;

public class UserModel
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
public class UserModelRegister
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public long RoleId { get; set; } 
    public bool IsWoman { get; set; } = false;
}
