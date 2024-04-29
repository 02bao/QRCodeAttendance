namespace QRCodeAttendance.Application.Position;

public class PositionDTO
{
    public long Id { get; set; }
    public string PositionName { get; set; } = "";
    public string Description { get; set; } = "";
    public int EmployeeCount { get; set; }
}

