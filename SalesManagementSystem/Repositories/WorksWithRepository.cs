using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.WorksWithDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Repositories
{
    public class WorksWithRepository : IWorksWithRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        public WorksWithRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<WorksWith>> GetAllWorksWithAsync()
        {
            return await _context.WorksWiths.ToListAsync();
        }

        public async Task<List<WorksWith>> GetAllWorksWithByEmployeeIdAsync(int employeeId)
        {
            return await _context.WorksWiths.Where(ww => ww.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<List<WorksWith>> GetAllWorksWithByClientIdAsync(int clientId)
        {
            return await _context.WorksWiths.Where(ww => ww.ClientId == clientId).ToListAsync();
        }

        public async Task<WorksWith?> GetByIdWorksWithAsync(int employeeId, int clientId)
        {
            return await _context.WorksWiths.FirstOrDefaultAsync(ww => ww.EmployeeId == employeeId && ww.ClientId == clientId);
        }

        public async Task<bool> ProcessTransactionAsync(TransactionRequestDto request)
        {
            if (_cache.TryGetValue(request.IdempotencyKey, out _))
            {
                return true;
            }
            using var dbTransaction = await _context.Database.BeginTransactionAsync(
                System.Data.IsolationLevel.Serializable);

            try
            {
                var historyEntry = new Transaction
                {
                    // Fix the names of buyer and seller to employee and client
                    IdempotencyKey = request.IdempotencyKey,
                    EmployeeId = request.EmployeeId,
                    ClientId = request.ClientId,
                    TransactionAmount = request.TransactionAmount,
                    CreatedAt = DateTime.UtcNow
                };
                _context.TransactionHistories.Add(historyEntry);

                var worksWith = await _context.WorksWiths
                    .FindAsync(request.EmployeeId, request.ClientId);

                if (worksWith != null)
                {
                    worksWith.TotalSales += request.TransactionAmount;
                }
                else
                {
                    var newWorksWith = new WorksWith
                    {
                        EmployeeId = request.EmployeeId,
                        ClientId = request.ClientId,
                        TotalSales = request.TransactionAmount
                    };
                    await _context.WorksWiths.AddAsync(newWorksWith);
                }

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                _cache.Set(request.IdempotencyKey, true, TimeSpan.FromDays(1));
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("Unique") == true)
            {
                await dbTransaction.RollbackAsync();
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                return false;
            }
            return true;
        }
    }
}
