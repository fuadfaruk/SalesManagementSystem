using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IBranchRepository
    {
        List<Branch> GetAllBranch();
        Branch? GetBranchById(int branchId);
        public Branch? GetBranchByManagerId(int managerId);
        void AddBranch(Branch branch);
        void UpdateBranch(Branch branch);
        void DeleteBranch(Branch branch);
    }
}
