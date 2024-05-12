using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlCompany
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public SqlFile? Images { get; set; } = null;
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime MaxLateTime { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}
