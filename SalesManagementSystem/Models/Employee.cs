using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public DateOnly BirthDay { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
        public string? Sex { get; set; }
        public int? SupervisorId { get; set; }
        public Employee? Supervisor { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
