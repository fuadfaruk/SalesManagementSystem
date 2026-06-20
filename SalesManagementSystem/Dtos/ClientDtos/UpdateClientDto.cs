namespace SalesManagementSystem.Dtos.ClientDtos
{
    public class UpdateClientDto
    {
        public required string ClientName { get; set; }
        public int BranchId { get; set; }
    }
}
