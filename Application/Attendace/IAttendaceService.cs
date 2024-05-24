namespace QRCodeAttendance.Application.Attendace;

public interface IAttendaceService
{
    Task<bool> CheckIn(long UserId,long CompanyId);
    Task<AttendanceGetByUser> GetByUserId(long UserId, DateTime date); 
    Task<AttendanceGetByUserInMonth> GetByUserInMonth(long UserId, string month);
}
