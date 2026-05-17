using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public required string ClientName { get; set; }
        public int BranchId { get; set; }
        public required Branch Branch { get; set; }
    }
}
