using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class GetByIdDetailedInfoBranchDto
    {
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public GetByIdShortInfoEmployee? Manager { get; set; }
        public DateOnly ManagerStartDate { get; set; }
    }
}
