using SalesManagementSystem.Models;

namespace SalesManagementSystem.Dtos.ClientDtos
{
    public class GetByIdClientDto
    {
        public int ClientId { get; set; }
        public required string ClientName { get; set; }
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
