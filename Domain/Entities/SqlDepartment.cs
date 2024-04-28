using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlDepartment
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int TotalEmployees { get; set; } = 0;
    public int TotalPositions { get; set; } = 0;
    public List<SqlPosition>? Position { get; set; }
}
