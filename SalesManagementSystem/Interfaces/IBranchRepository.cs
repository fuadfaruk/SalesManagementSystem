using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IBranchRepository
    {
        public List<Branch> GetAllBranch();
        public Branch? GetBranchById(int empId);
        public void AddBranch(Branch branch);
        public void UpdateBranch(Branch branch);
        public void DeleteBranch(Branch branch);
    }
}
