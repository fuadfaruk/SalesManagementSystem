using SalesManagementSystem.Dtos.BranchSupplierDtos;

namespace SalesManagementSystem.Mapper
{
    public static class BranchSupplierMapper
    {
        public static GetBranchSupplierDto ToGetAllBranchSupplierDto(this Models.BranchSupplier branchSupplier)
        {
            return new GetBranchSupplierDto
            {
                BranchId = branchSupplier.BranchId,
                SupplierName = branchSupplier.SupplierName,
                SupplyType = branchSupplier.SupplyType
            };
        }
    }
}
