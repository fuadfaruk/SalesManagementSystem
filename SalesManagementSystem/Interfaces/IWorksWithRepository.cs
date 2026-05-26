using SalesManagementSystem.Dtos.WorksWithDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IWorksWithRepository
    {
        Task<List<WorksWith>> GetAllWorksWithAsync();
        Task<List<WorksWith>> GetAllWorksWithByEmployeeIdAsync(int employeeId);
        Task<List<WorksWith>> GetAllWorksWithByClientIdAsync(int clientId);
        Task<WorksWith?> GetByIdWorksWithAsync(int employeeId, int clientId);
        Task<bool> ProcessTransactionAsync(TransactionRequestDto request);
    }
}
