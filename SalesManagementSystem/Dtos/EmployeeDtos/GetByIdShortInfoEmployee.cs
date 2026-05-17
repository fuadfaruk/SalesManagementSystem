using SalesManagementSystem.Dtos.BranchDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.EmployeeDtos
{
    public class GetByIdShortInfoEmployee
    {
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
