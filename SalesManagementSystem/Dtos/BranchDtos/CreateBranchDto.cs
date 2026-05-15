using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class CreateBranchDto
    {
        public required string branch_name { get; set; }
        public int? mgr_id { get; set; }
        public DateOnly mgr_start_date { get; set; }
    }
}
