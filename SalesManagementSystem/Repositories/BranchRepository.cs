using Microsoft.EntityFrameworkCore;
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
        public async Task<Branch?> GetBranchByIdAsync(int branchId)
        {
            return await _context.Branches.SingleOrDefaultAsync(e => e.BranchId == branchId);
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
        public async Task DeleteBranchAsync(Branch branch)
        {
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return;
        }
    }
}
