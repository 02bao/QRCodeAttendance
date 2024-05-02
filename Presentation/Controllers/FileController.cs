using Microsoft.AspNetCore.Mvc;
using QRCodeAttendance.Application.File;

namespace QRCodeAttendance.Presentation.Controllers;

public class FileController(IFileService fileService) : BaseController
{
    [HttpPost("")]
    public async Task<IActionResult> CreateFile([FromForm] IFormFile file)
    {
        long FileId = await fileService.CreateFile(file);
        return FileId > 0 ? Ok(FileId) : BadRequest();
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetFile(long Id)
    {
        string file = await fileService.GetFile(Id);
        return string.IsNullOrEmpty(file) ? Ok(file) : NotFound();
    }
}
