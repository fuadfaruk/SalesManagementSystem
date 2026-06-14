using SalesManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Dtos.WorksWithDtos
{
    public class GetWorksWithDto
    {
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public decimal TotalSales { get; set; }
        public Employee Employee { get; set; } = null!;
        public Client? Client { get; set; } = null!;
    }
}
