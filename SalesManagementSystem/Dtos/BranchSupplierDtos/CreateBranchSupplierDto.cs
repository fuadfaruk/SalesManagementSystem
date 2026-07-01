using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchSupplierDtos
{
    public class CreateBranchSupplierDto
    {
        [ForeignKey(nameof(BranchId))]
        public int BranchId { get; set; }
        public required string SupplierName { get; set; }
        public string? SupplyType { get; set; }
    }
}
