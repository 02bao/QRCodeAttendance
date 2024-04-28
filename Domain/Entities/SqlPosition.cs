namespace QRCodeAttendance.Domain.Entities;

public class SqlPosition
{
    public long Id { get; set; }
    public SqlDepartment? Department { get; set; }
    public string PositionName { get; set; } = "";
    public string Description { get; set; } = "";
    public int EmployeeCount { get; set; } = 0;
}
