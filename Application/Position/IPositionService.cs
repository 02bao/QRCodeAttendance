using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Application.Position
{
    public interface IPositionService
    {
        Task<bool> CreateNewPositions(long DepartmentId, PositionCreate Create);
        Task<List<SqlPosition>> GetAll();
        Task<SqlPosition> GetById(long Id);
        Task<List<SqlPosition>> GetByDepartmentId(long DepartmentId);  
        Task<bool> Update(long PositionId, PositionUpdate Positions);
        Task<bool > Delete(long Id);
    }
}
