using Microsoft.EntityFrameworkCore;
using System.Threading;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Repositories
{
    public class ClientRepository : IClientRepository
    {
        ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllClientsAsync(QueryObject query, CancellationToken cancellationToken = default)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Clients.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Client?> GetClientByIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == clientId, cancellationToken);
        }

        public async Task AddClientAsync(Client client, CancellationToken cancellationToken = default)
        {
            await _context.Clients.AddAsync(client, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return;
        }

        public async Task<bool> UpdateClientAsync(int clientId, UpdateClientDto updateClientDto, CancellationToken cancellationToken = default)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c =>c.ClientId == clientId, cancellationToken);
            if (client == null)
            {
                return false;
            }
            client.ToClientFromUpdateClientDto(updateClientDto);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task DeleteClientAsync(Client client, CancellationToken cancellationToken = default)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync(cancellationToken);

            return;
        }
    }
}
