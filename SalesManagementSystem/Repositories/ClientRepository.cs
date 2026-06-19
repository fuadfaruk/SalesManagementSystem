using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.ClientDtos;
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

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int clientId)
        {
            return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task AddClientAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<bool> UpdateClientAsync(int clientId, UpdateClientDto updateClientDto)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c =>c.ClientId == clientId);
            if (client == null)
            {
                return false;
            }
            client.ToClientFromUpdateClientDto(updateClientDto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteClientAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return;
        }
    }
}
