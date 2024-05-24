using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Company;

public class CompanyDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Images { get; set; } = "";
    public TimeSpan StartTime { get; set; } = DateTime.UtcNow.TimeOfDay;
    public TimeSpan MaxLateTime { get; set; } = DateTime.UtcNow.TimeOfDay;
}
