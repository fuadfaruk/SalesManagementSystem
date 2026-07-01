using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Dtos.BranchSupplierDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IBranchSupplierRepository
    {
        Task<List<BranchSupplier>> GetAllBranchSuppliersAsync(QueryObject query, CancellationToken cancellationToken);
        Task<List<BranchSupplier>> GetAllBranchSupplierByIdAsync(int branchId, CancellationToken cancellationToken);
        Task<BranchSupplier?> GetBranchSupplierByIdNSupplierNameAsync(int branchSupplierId, string supplierName, CancellationToken cancellationToken);
        Task AddBranchSupplierAsync(BranchSupplier branchSupplier, CancellationToken cancellationToken);
        Task<bool> UpdateBranchSupplierAsync(int branchId, string supplierName, UpdateBranchSupplierDto updatedBranchSupplier, CancellationToken cancellationToken = default);
        Task DeleteBranchSupplierAsync(BranchSupplier branchSupplier, CancellationToken cancellationToken);
    }
}
