using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Dtos.AccountDtos
{
    public class RegisterDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
