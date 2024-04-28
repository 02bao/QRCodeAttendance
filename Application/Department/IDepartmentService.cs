using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Department;

public interface IDepartmentService
{
    Task<bool> CreateNewDepartment(string Name, string Description);
    Task<List<SqlDepartment>> GetAll();
    Task<SqlDepartment?> GetById(long Id);
    Task<bool> Update(SqlDepartment Updates);
    Task<bool> DeleteById(long Id);
}
