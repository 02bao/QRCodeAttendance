using QRCodeAttendance.Application.User;

namespace QRCodeAttendance.Application.Department;

public class DepartmentDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}
public class DepartmentItemDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int PositionCount { get; set; }
    public int EmployeeCount { get; set; }
}

public class DepartmentGetAttendance
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public List<UserGetAttendace> User { get; set; } = new List<UserGetAttendace>();
}