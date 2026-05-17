using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();
        Client? GetClientById(int clientId);
        void AddClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
