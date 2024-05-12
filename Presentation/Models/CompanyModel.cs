using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Presentation.Models;

public class CompanyModel
{
}

public class CompanyCreateModel
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime MaxLateTime { get; set; } = DateTime.UtcNow;
}

public class CompanyUpdateModel
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public long FileId { get; set; } = -1;
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime MaxLateTime { get; set; } = DateTime.UtcNow;
}
