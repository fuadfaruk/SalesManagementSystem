using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Dtos.AccountDtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
