using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlUser
{
    [Key]
    public long Id { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsWoman { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public SqlFile? Images { get; set; } = null;
    public bool IsVerified { get; set; } = false;
    public string VerifyToken { get; set; } = string.Empty;
    public long RoleId { get; set; }
    public SqlRole Role { get; set; } = null!;
    public SqlPosition? Position { get; set; } = null;
    public SqlDepartment? Department { get; set; } = null;
    public List<SqlToken> Tokens { get; set; } = [];
}