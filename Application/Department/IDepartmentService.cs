using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Department;

public interface IDepartmentService
{
    Task<bool> CreateNewDepartment(DepartmentCreate Create);
    Task<List<SqlDepartment>> GetAll();
    Task<SqlDepartment> GetById(long Id);
    Task<bool> Update(long DepartmentId, DepartmentUpdate Departments);
    Task<bool> DeleteById(long Id);
}
