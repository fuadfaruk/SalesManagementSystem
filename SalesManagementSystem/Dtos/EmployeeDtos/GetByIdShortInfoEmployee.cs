using SalesManagementSystem.Dtos.BranchDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.EmployeeDtos
{
    public class GetByIdShortInfoEmployee
    {
        public int emp_id { get; set; }
        public required string first_name { get; set; }
        public string? last_name { get; set; }
    }
}
