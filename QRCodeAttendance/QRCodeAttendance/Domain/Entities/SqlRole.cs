using System.ComponentModel.DataAnnotations;

namespace QRCodeAttendance.QRCodeAttendance.Domain.Entities;

public class SqlRole
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsDeleted { get; set; } = false;
    public List<SqlUser> Users { get; set; } = [];
}