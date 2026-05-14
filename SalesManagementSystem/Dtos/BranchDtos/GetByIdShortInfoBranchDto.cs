using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class GetByIdShortInfoBranchDto
    {
        [Key]
        public int branch_id { get; set; }
        public required string branch_name { get; set; }
        public int? mgr_id { get; set; }
    }
}
