namespace QRCodeAttendance.Application.Position;

public class PositionDto
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}
public class PositionItemDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int EmployeeCount { get; set; }
}

public class GetPositionsByDepartmentIdDTO
{
    public string DepartmentName { get; set; } = "";
    public List<PositionItemDTO> Data { get; set; } = new List<PositionItemDTO>();
}