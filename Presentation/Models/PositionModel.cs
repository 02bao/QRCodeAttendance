
namespace QRCodeAttendance.Presentation.Models;

public class PositionModel
{
}
public class PositionCreateModel
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public class PositionUpdateModel
{
    public string? Name { get; set; } = "";
    public string? Description { get; set; } = "";
}
