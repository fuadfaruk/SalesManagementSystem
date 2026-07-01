using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.BranchSupplierDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Repositories
{
    public class BranchSupplierRepository : IBranchSupplierRepository
    {
        private readonly ApplicationDbContext _context;
        public BranchSupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BranchSupplier>> GetAllBranchSuppliersAsync(QueryObject query, CancellationToken cancellationToken = default)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await _context.BranchSuppliers.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<List<BranchSupplier>> GetAllBranchSupplierByIdAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _context.BranchSuppliers.AsNoTracking().Where(bs => bs.BranchId == branchId).ToListAsync(cancellationToken);
        }

        public async Task<BranchSupplier?> GetBranchSupplierByIdNSupplierNameAsync(int branchId, string supplierName, CancellationToken cancellationToken = default)
        {
            return await _context.BranchSuppliers.AsNoTracking().FirstOrDefaultAsync(bs => bs.BranchId == branchId && bs.SupplierName == supplierName, cancellationToken);
        }

        public async Task AddBranchSupplierAsync(BranchSupplier branchSupplier, CancellationToken cancellationToken = default)
        {
            await _context.BranchSuppliers.AddAsync(branchSupplier, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }
        
        public async Task<bool> UpdateBranchSupplierAsync(int branchId, string supplierName, UpdateBranchSupplierDto updatedBranchSupplier, CancellationToken cancellationToken = default)
        {
            var branchSupplier = await _context.BranchSuppliers.FirstOrDefaultAsync(bs => bs.BranchId == branchId && bs.SupplierName == supplierName, cancellationToken);
            if (branchSupplier == null)
            {
                return false;
            }
            branchSupplier.SupplyType = updatedBranchSupplier.SupplyType;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task DeleteBranchSupplierAsync(BranchSupplier branchSupplier, CancellationToken cancellationToken = default)
        {
            _context.BranchSuppliers.Remove(branchSupplier);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
