namespace QRCodeAttendance.Application.Department;

public class DepartmentDTO
{
}
public class DepartmentItemDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int PositionCount { get; set; }
}