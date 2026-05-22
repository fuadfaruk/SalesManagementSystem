using SalesManagementSystem.Data;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

// Fix return codes (Add details of the object when returned)
namespace SalesManagementSystem.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;
        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Branch> GetAllBranch()
        {
            return _context.Branches.ToList();
        }
        public Branch? GetBranchById(int branchId)
        {
            return _context.Branches.SingleOrDefault(e => e.BranchId == branchId);
        }
        public Branch? GetBranchByManagerId(int managerId)
        {
            return _context.Branches.FirstOrDefault(e => e.ManagerId == managerId);
        }
        public void AddBranch(Branch branch)
        {
            _context.Branches.Add(branch);
            _context.SaveChanges();

            return;
        }
        public void UpdateBranch(Branch branch)
        {
            _context.Branches.Update(branch);
            _context.SaveChanges();

            return;
        }
        public void DeleteBranch(Branch branch)
        {
            _context.Branches.Remove(branch);
            _context.SaveChanges();

            return;
        }
    }
}
