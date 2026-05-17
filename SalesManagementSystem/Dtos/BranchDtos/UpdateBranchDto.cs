namespace SalesManagementSystem.Dtos.BranchDtos
{
    public class UpdateBranchDto
    {
        public required string BranchName { get; set; }
        public int? ManagerId { get; set; }
        public DateOnly ManagerStartDate { get; set; }
    }
}
