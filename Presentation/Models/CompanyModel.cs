using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Presentation.Models;

public class CompanyModel
{
}

public class CompanyCreateModel
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string StartTime { get; set; } = "";
    public string MaxLateTime { get; set; } = "";
}

public class CompanyUpdateModel
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public long FileId { get; set; } = -1;
    public string StartTime { get; set; } = "";
    public string MaxLateTime { get; set; } = "";
}
