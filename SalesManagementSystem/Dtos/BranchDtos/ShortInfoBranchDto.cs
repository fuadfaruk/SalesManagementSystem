namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class ShortInfoBranchDto
    {
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public int? ManagerId { get; set; }
    }
}
