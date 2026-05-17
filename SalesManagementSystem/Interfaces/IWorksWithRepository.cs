using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IWorksWithRepository
    {
        List<WorksWith> GetAllWorksWith();
        List<WorksWith> GetAllWorksWithByEmployeeId(int employeeId);
        List<WorksWith> GetAllWorksWithByClientId(int clientId);
        WorksWith? GetByIdWorksWith(int employeeId, int clientId);
        void AddWorksWith(WorksWith worksWith);
        void UpdateWorksWith(WorksWith worksWith);
        void DeleteWorksWith(WorksWith worksWith);
    }
}
