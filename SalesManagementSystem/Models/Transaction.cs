using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementSystem.Models
{
    // This model is not used in the database table but this makes the database design more elegant
    public class Transaction
    {
        public int TransactionID { get; set; }
        [MaxLength(50)]
        public required string IdempotencyKey { get; set; } // This field seems redundant to me but after counciling with GPT for 40 mintues it suggested me to keep it
        public required int EmployeeId { get; set; }
        public required int ClientId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal TransactionAmount { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
