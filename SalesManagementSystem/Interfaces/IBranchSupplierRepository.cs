using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IBranchSupplierRepository
    {
        Task<List<BranchSupplier>> GetAllBranchSuppliersAsync(QueryObject query, CancellationToken cancellationToken);
        Task<BranchSupplier?> GetBranchSupplierByIdAsync(int branchId, string supplierName, CancellationToken cancellationToken);
        Task AddBranchSupplierAsync(BranchSupplier branchSupplier, CancellationToken cancellationToken);
        Task<bool> UpdateBranchSupplierAsync(int branchId, string supplierName, BranchSupplier updatedBranchSupplier, CancellationToken cancellationToken);
        Task DeleteBranchSupplierAsync(BranchSupplier branchSupplier, CancellationToken cancellationToken);
    }
}
