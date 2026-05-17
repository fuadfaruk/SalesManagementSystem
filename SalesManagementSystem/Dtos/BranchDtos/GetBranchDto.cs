using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class GetBranchDto
    {
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public int? ManagerId { get; set; }
        public DateOnly ManagerStartDate { get; set; }
    }
}
