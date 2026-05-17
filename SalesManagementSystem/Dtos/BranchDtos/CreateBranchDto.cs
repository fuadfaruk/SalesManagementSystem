using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class CreateBranchDto
    {
        public required string BranchName { get; set; }
        public int? ManagerId { get; set; }
        public DateOnly ManagerStartDate { get; set; }
    }
}
