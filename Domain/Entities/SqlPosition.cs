namespace QRCodeAttendance.Domain.Entities;

public class SqlPosition
{
    public long Id { get; set; }
    public SqlDepartment Department { get; set; } = null!;
    public List<SqlUser> User { get; set; } = new List<SqlUser>();
    public string PositionName { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsDeleted { get; set; } = false;
}
