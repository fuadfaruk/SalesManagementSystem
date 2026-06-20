namespace SalesManagementSystem.Dtos.ClientDtos
{
    public class CreateClientDto
    {
        public required string ClientName { get; set; }
        public int BranchId { get; set; }
    }
}
