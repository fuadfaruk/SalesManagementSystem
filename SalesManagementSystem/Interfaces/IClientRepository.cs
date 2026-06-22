using System.Threading;
using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClientsAsync(QueryObject query, CancellationToken cancellationToken = default);
        Task<Client?> GetClientByIdAsync(int clientId, CancellationToken cancellationToken = default);
        Task AddClientAsync(Client client, CancellationToken cancellationToken = default);
        Task<bool> UpdateClientAsync(int clientId, UpdateClientDto updateClientDto, CancellationToken cancellationToken = default);
        Task DeleteClientAsync(Client client, CancellationToken cancellationToken = default);
    }
}
