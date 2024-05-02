
namespace QRCodeAttendance.Application.File;

public interface IFileService
{
    Task<long> CreateFile(IFormFile file);
    Task<string> GetFile(long Id);
}