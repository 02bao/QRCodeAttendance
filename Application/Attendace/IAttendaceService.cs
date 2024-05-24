namespace QRCodeAttendance.Application.Attendace;

public interface IAttendaceService
{
    Task<bool> CheckIn(long UserId, long DepartmentId,long CompanyId);
    Task<AttendanceGetByUser> GetByUserId(long USerId); 
}
