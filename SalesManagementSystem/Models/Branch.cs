using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Models
{
    // Change Dtos everytime after changing the model
    public class Branch
    {
        [Key]
        public int branch_id { get; set; }
        public required string branch_name { get; set; }
        public int? mgr_id { get; set; }
        [ForeignKey(nameof(mgr_id))]
        public Employee Manager { get; set; } = null!;
        public DateOnly mgr_start_date { get; set; }
    }
}
