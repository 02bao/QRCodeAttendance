
namespace QRCodeAttendance.Application.Cloud;

public interface ICloudinaryService
{
    bool DeleteFile(string ImageId);
    string uploadFile(IFormFile file);
}