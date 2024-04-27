using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlUser
{
    [Key]
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public bool IsDeleted { get; set; } = false;
    public long RoleId { get; set; }
    public SqlRole Role { get; set; } = null!;
    public List<SqlToken> Tokens { get; set; } = [];
}