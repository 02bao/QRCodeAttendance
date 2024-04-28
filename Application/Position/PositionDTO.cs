namespace QRCodeAttendance.Application.Position;

public class PositionDTO
{
}
public class PositionCreate
{
    public string PositionName { get; set; } = "";
    public string Description { get; set; } = "";
}

public class PositionUpdate
{
    public string PositionName { get; set; } = "";
    public string Description { get; set; } = "";
    public int EmployeeCount { get; set; } = 0;
}
