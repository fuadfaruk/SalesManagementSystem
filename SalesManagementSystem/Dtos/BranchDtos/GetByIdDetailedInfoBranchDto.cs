using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class GetByIdDetailedInfoBranchDto
    {
        [Key]
        public int branch_id { get; set; }
        public required string branch_name { get; set; }
        public Employee Manager { get; set; } = null!;
        public DateOnly mgr_start_date { get; set; }
    }
}
