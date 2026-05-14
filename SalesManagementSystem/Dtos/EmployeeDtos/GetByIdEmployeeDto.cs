using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.EmployeeDtos
{
    public class GetByIdEmployeeDto
    {
        [Key]
        public int emp_id { get; set; }
        public DateOnly birth_date { get; set; }
        public required string first_name { get; set; }
        public string? last_name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal salary { get; set; }
        public string? sex { get; set; }
        [ForeignKey("super_id")]
        public int? super_id { get; set; }
        public int branch_id { get; set; }
    }
}
