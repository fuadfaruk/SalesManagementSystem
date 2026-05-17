using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Models
{
    [PrimaryKey(nameof(EmployeeId), nameof(ClientId))]
    public class WorksWith
    {
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSales { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; } = null!;
    }
}
