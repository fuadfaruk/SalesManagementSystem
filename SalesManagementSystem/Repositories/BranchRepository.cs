using Microsoft.EntityFrameworkCore;
using System.Threading;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using SalesManagementSystem.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesManagementSystem.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;
        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Branch>> GetAllBranchAsync(QueryObject query, CancellationToken cancellationToken = default)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Branches.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<Branch?> GetBranchByIdAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _context.Branches.AsNoTracking().SingleOrDefaultAsync(e => e.BranchId == branchId, cancellationToken);
        }
        public async Task<Branch?> GetBranchByManagerIdAsync(int managerId, CancellationToken cancellationToken = default)
        {
            return await _context.Branches.AsNoTracking().FirstOrDefaultAsync(e => e.ManagerId == managerId, cancellationToken);
        }
        public async Task AddBranchAsync(Branch branch, CancellationToken cancellationToken = default)
        {
            await _context.Branches.AddAsync(branch, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return;
        }
        public async Task<bool> UpdateBranchAsync(int branchId, UpdateBranchDto updateBranchDto, CancellationToken cancellationToken = default)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.BranchId == branchId, cancellationToken);
            if (branch == null)
            {
                return false;
            }
            branch.ToBranchFromUpdateBranchDto(updateBranchDto);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task DeleteBranchAsync(Branch branch, CancellationToken cancellationToken = default)
        {
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync(cancellationToken);

            return;
        }
    }
}
