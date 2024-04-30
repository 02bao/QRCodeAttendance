using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Serilog;
namespace QRCodeAttendance.Application.Cloud;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryService()
    {
        var CloudinaryAccount = new Account(
            "dbc7m2bfe",
            "414433899294356",
           "Ftv4-eQroBMkGXO9oEmAshTn5M0"
        );
        _cloudinary = new Cloudinary(CloudinaryAccount);
        _cloudinary.Api.Secure = true;
    }

    public string uploadFile(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return "";
            }
            using (var stream = file.OpenReadStream())
            {
                ImageUploadParams uploadParams = new()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = Guid.NewGuid().ToString(),
                    Folder = "qrAttendance",
                };
                ImageUploadResult result = _cloudinary.Upload(uploadParams);
                if (result.Error != null && result != null)
                {
                    Log.Error(result.Error.ToString() ?? "Upload File to Cloudinary fail!");
                    return "";
                }
                if (result == null)
                {
                    Log.Error("Upload File to Cloudinary return null");
                    return "";
                }
                return result.SecureUrl.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return "";
        }
    }

    public bool DeleteFile(string ImageId)
    {
        try
        {
            var deleteParams = new DeletionParams(ImageId);

            var result = _cloudinary.Destroy(deleteParams);

            if (result.Error != null)
            {
                Log.Error(result.Error.Message ?? "Delete File from Cloudinary failed!");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return false;
        }
    }
}
