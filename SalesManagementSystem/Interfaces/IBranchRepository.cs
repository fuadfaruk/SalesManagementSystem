using System.Threading;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetAllBranchAsync(QueryObject query, CancellationToken cancellationToken = default);
        Task<Branch?> GetBranchByIdAsync(int branchId, CancellationToken cancellationToken = default);
        Task<Branch?> GetBranchByManagerIdAsync(int managerId, CancellationToken cancellationToken = default);
        Task AddBranchAsync(Branch branch, CancellationToken cancellationToken = default);
        Task<bool> UpdateBranchAsync(int branchId, UpdateBranchDto updateBranchDto, CancellationToken cancellationToken = default);
        Task DeleteBranchAsync(Branch branch, CancellationToken cancellationToken = default);
    }
}
