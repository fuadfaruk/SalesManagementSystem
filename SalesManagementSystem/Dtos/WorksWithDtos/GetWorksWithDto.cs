using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;

namespace SalesManagementSystem.Dtos.WorksWithDtos
{
    public class GetWorksWithDto
    {
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public decimal TotalSales { get; set; }
        public ShortInfoEmployeeDto Employee { get; set; } = null!;
        public GetClientDto? Client { get; set; } = null!;
    }
}
