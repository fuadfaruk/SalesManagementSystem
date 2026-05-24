using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetAllBranchAsync();
        Task<Branch?> GetBranchByIdAsync(int branchId);
        Task<Branch?> GetBranchByManagerIdAsync(int managerId);
        Task AddBranchAsync(Branch branch);
        Task<bool> UpdateBranchAsync(int branchId, UpdateBranchDto updateBranchDto);
        Task DeleteBranchAsync(Branch branch);
    }
}
