namespace QRCodeAttendance.Application.Department;

public interface IDepartmentService
{
    Task<bool> CreateNewDepartment(string Name, string Description);
    Task<List<DepartmentItemDTO>> GetAll();
    Task<DepartmentItemDTO?> GetById(long Id);
    Task<bool> Update(long DepartmentId, string? Name, string? Description);
    Task<bool> DeleteById(long Id);
}
