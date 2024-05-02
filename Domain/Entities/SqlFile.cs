using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.Domain.Entities;

public class SqlFile
{
    [Key]
    public long Id { get; set; }
    public string Path { get; set; } = "";
    public FileType Type { get; set; }
}
public enum FileType
{
    Image = 1,
    Video = 2,
    Audio = 3
}