using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlToken
{
    [Key]
    public long Id { get; set; }
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public DateTime CreateTime { get; set; }
    public DateTime ExpiredTime { get; set; }
    public bool IsExpired { get; set; } = false;
    public long UserId { get; set; }
    public SqlUser User { get; set; } = null!;
}