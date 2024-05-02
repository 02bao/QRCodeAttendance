namespace QRCodeAttendance.Domain.Entities;

public class SqlPosition
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsDeleted { get; set; } = false;
    public SqlDepartment? Department { get; set; } = null;
    public List<SqlUser> Users { get; set; } = new List<SqlUser>();
}
