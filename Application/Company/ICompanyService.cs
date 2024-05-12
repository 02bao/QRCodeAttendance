namespace QRCodeAttendance.Application.Company;

public interface ICompanyService
{
    Task<bool> CreateNew(string Name, string Email, DateTime StartTime, DateTime MaxLateTime);
    Task<List<CompanyDTO>> GetAll();
    Task<CompanyDTO>? GetById(long Id);
    Task<bool> Update(long CompanyId,string Name,string Email, DateTime StartTime, DateTime MaxLateTime, long FileId);
    Task<bool> Delete(long Id);
}
