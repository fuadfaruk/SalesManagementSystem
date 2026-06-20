using SalesManagementSystem.Dtos.WorksWithDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IWorksWithRepository
    {
        Task<List<WorksWith>> GetAllWorksWithAsync(QueryObject query);
        Task<List<WorksWith>> GetAllWorksWithByEmployeeIdAsync(int employeeId);
        Task<List<WorksWith>> GetAllWorksWithByClientIdAsync(int clientId);
        Task<WorksWith?> GetByIdWorksWithAsync(int employeeId, int clientId);
        Task<(bool Success, bool Created, WorksWith? Entity)> ProcessTransactionAsync(TransactionRequestDto request);
    }
}
