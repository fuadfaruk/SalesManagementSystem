using SalesManagementSystem.Dtos.BranchSupplierDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Mapper
{
    public static class BranchSupplierMapper
    {
        public static GetBranchSupplierDto ToGetAllBranchSupplierDto(this BranchSupplier branchSupplier)
        {
            return new GetBranchSupplierDto
            {
                BranchId = branchSupplier.BranchId,
                SupplierName = branchSupplier.SupplierName,
                SupplyType = branchSupplier.SupplyType
            };
        }

        public static BranchSupplier ToBranchSupplier(this CreateBranchSupplierDto branchSupplierDto)
        {
            return new BranchSupplier
            {
                BranchId = branchSupplierDto.BranchId,
                SupplierName = branchSupplierDto.SupplierName,
                SupplyType = branchSupplierDto.SupplyType
            };
        }
    }
}
