using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlCompany
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public SqlFile? Images { get; set; } = null;
    public TimeSpan StartTime { get; set; } = DateTime.Now.TimeOfDay;
    public TimeSpan MaxLateTime { get; set; } = DateTime.Now.TimeOfDay;
    public bool IsDeleted { get; set; } = false;
}
