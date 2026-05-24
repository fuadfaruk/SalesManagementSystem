using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int clientId);
        Task AddClientAsync(Client client);
        Task<bool> UpdateClientAsync(int clientId, UpdateClientDto updateClientDto);
        Task DeleteClientAsync(Client client);
    }
}
