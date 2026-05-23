using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IBranchRepository
    {
        List<Branch> GetAllBranch();
        Task<Branch?> GetBranchByIdAsync(int branchId);
        public Branch? GetBranchByManagerId(int managerId);
        void AddBranch(Branch branch);
        void UpdateBranch(Branch branch);
        Task DeleteBranchAsync(Branch branch);
    }
}
