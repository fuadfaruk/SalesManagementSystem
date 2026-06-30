using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Models
{
    [PrimaryKey(nameof(BranchId), nameof(SupplierName))]
    public class BranchSupplier
    {
        [ForeignKey(nameof(BranchId))]
        public int BranchId { get; set; }
        public required string SupplierName { get; set; }
        public string? SupplyType { get; set; }
    }
}
