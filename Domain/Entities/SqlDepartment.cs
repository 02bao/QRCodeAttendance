using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlDepartment
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<SqlPosition> Positions { get; set; } = new List<SqlPosition>();
    public List<SqlUser> User { get; set; } = new List<SqlUser>();
    public bool IsDeleted { get; set; } = false;
}
