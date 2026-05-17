using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public DateOnly BirthDay { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
        public string? Sex { get; set; }
        public int? SupervisorId { get; set; } // There is better way to name this in EF conventions, but this is for learing how to use ForeignKey attribute ;)
        public Employee? Supervisor { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
