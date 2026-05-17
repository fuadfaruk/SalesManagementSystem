using SalesManagementSystem.Models;

namespace SalesManagementSystem.Dtos.ClientDtos
{
    public class GetAllClientDto
    {
        public int ClientId { get; set; }
        public required string ClientName { get; set; }
        public int BranchId { get; set; }
    }
}
