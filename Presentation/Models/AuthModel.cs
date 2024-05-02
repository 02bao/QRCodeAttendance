namespace QRCodeAttendance.Presentation.Models;

public class AuthModel
{
}
public class ResetPwdModel
{
    public long UserId { get; set; }
    public required string NewPassword { get; set; }
}