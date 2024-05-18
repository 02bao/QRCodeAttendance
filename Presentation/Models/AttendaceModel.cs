using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Presentation.Models;

public class AttendaceModel
{
}

public class AttendanceCheckInModel
{
    public long UserId { get; set; }
    public long DepartmentId { get; set; }
    public long CompanyId { get; set; }
}
