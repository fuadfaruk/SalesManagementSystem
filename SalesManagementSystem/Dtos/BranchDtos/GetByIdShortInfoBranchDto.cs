using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class GetByIdShortInfoBranchDto
    {
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public int? ManagerId { get; set; }
    }
}
