using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos
{
    public class UpdateEmployeeDto
    {
        public DateOnly birth_date { get; set; }
        public required string first_name { get; set; }
        public string? last_name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal salary { get; set; }
        public string? sex { get; set; }
    }
}
