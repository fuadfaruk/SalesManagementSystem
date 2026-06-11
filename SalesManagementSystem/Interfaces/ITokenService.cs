using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
