namespace SalesManagementSystem.Models
{
    // Change Dtos everytime after changing the model
    public class Branch
    {
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public int? ManagerId { get; set; }
        public DateOnly ManagerStartDate { get; set; }
        public Employee? Manager { get; set; }
    }
}
