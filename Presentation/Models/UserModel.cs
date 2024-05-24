namespace QRCodeAttendance.Presentation.Models;

public class UserModel
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
public class UserCreateModel
{
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Password { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsWoman { get; set; } = false;
    public long RoleId { get; set; }
}

public class UserUpdateModel
{
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsWoman { get; set; } = false;
    public long RoleId { get; set; }
    public long FileId { get; set; } = -1;
}