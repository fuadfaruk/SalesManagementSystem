namespace SalesManagementSystem.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public required string ClientName { get; set; }
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
