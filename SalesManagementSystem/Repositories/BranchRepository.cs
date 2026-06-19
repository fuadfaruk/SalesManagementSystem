using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
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
        public async Task<List<Branch>> GetAllBranchAsync()
        {
            return await _context.Branches.AsNoTracking().ToListAsync();
        }
        public async Task<Branch?> GetBranchByIdAsync(int branchId)
        {
            return await _context.Branches.AsNoTracking().SingleOrDefaultAsync(e => e.BranchId == branchId);
        }
        public async Task<Branch?> GetBranchByManagerIdAsync(int managerId)
        {
            return await _context.Branches.AsNoTracking().FirstOrDefaultAsync(e => e.ManagerId == managerId);
        }
        public async Task AddBranchAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task<bool> UpdateBranchAsync(int branchId, UpdateBranchDto updateBranchDto)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.BranchId == branchId);
            if (branch == null)
            {
                return false;
            }
            branch.ToBranchFromUpdateBranchDto(updateBranchDto);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task DeleteBranchAsync(Branch branch)
        {
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return;
        }
    }
}
