using SalesManagementSystem.Dtos.EmployeeDtos;

namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class DetailedInfoBranchDto
    {
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public ShortInfoEmployeeDto? Manager { get; set; }
        public DateOnly ManagerStartDate { get; set; }
    }
}
