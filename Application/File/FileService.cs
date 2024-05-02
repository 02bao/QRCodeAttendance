using QRCodeAttendance.Application.Cloud;
using QRCodeAttendance.Domain.Entities;
using QRCodeAttendance.Infrastructure.Data;
using Serilog;

namespace QRCodeAttendance.Application.File;

public class FileService(CloudinaryService cloudinaryService, DataContext _context) : IFileService
{
    public async Task<long> CreateFile(IFormFile file)
    {
        try
        {
            if (file == null)
            {
                return -1;
            }
            string path = cloudinaryService.uploadFile(file);
            SqlFile newFile = new()
            {
                Path = path
            };
            await _context.Files.AddAsync(newFile);
            await _context.SaveChangesAsync();
            return newFile.Id;
        }

        catch (Exception)
        {
            Log.Error("Failed to create file");
            return -1;

        }
    }
    public async Task<string> GetFile(long Id)
    {
        SqlFile? file = await _context.Files.FindAsync(Id);
        return file == null ? "" : file.Path;
    }
}
