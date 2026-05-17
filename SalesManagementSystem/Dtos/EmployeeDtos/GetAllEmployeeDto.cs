using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.EmployeeDtos
{
    public class GetAllEmployeeDto
    {
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        [ForeignKey("super_id")]
        public int? SupervisorId { get; set; }
        public int? BranchId { get; set; }
    }
}
