using SalesManagementSystem.Dtos.BranchDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.EmployeeDtos
{
    public class DetailedInfoEmployeeDto
    {
        public int EmployeeId { get; set; }
        public DateOnly BirthDate { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
        public string? Sex { get; set; }
        public int? SuperId { get; set; }
        public ShortInfoBranchDto? Branch { get; set; }
    }
}
