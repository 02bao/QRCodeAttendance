
namespace QRCodeAttendance.Presentation.Models;

public class PositionModel
{
}
public class PositionCreateModel
{
    public string PositionName { get; set; } = "";
    public string Description { get; set; } = "";
}

public class PositionUpdateModel
{
    public string? PositionName { get; set; } = "";
    public string? Description { get; set; } = "";
}
