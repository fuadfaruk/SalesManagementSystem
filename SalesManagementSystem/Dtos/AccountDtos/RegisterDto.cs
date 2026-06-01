using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
