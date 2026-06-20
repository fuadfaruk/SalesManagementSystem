using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.WorksWithDtos;
using SalesManagementSystem.Helpers;
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

        public async Task<List<WorksWith>> GetAllWorksWithAsync(QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.WorksWiths.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync();
        }

        public async Task<List<WorksWith>> GetAllWorksWithByEmployeeIdAsync(int employeeId)
        {
            return await _context.WorksWiths.AsNoTracking().Where(ww => ww.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<List<WorksWith>> GetAllWorksWithByClientIdAsync(int clientId)
        {
            return await _context.WorksWiths.AsNoTracking().Where(ww => ww.ClientId == clientId).ToListAsync();
        }

        public async Task<WorksWith?> GetByIdWorksWithAsync(int employeeId, int clientId)
        {
            return await _context.WorksWiths.AsNoTracking().FirstOrDefaultAsync(ww => ww.EmployeeId == employeeId && ww.ClientId == clientId);
        }

        public async Task<(bool Success, bool Created, WorksWith? Entity)> ProcessTransactionAsync(TransactionRequestDto request)
        {
            if (_cache.TryGetValue(request.IdempotencyKey, out _))
            {
                var existing = await _context.WorksWiths.FindAsync(request.EmployeeId, request.ClientId);
                return (true, false, existing);
            }
            using var dbTransaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                var historyEntry = new Transaction
                {
                    IdempotencyKey = request.IdempotencyKey,
                    EmployeeId = request.EmployeeId,
                    ClientId = request.ClientId,
                    TransactionAmount = request.TransactionAmount,
                    CreatedAt = DateTime.UtcNow
                };
                _context.TransactionHistories.Add(historyEntry);

                var worksWith = await _context.WorksWiths.FindAsync(request.EmployeeId, request.ClientId);

                bool created = false;
                if (worksWith != null)
                {
                    worksWith.TotalSales += request.TransactionAmount;
                }
                else
                {
                    created = true;
                    worksWith = new WorksWith
                    {
                        EmployeeId = request.EmployeeId,
                        ClientId = request.ClientId,
                        TotalSales = request.TransactionAmount
                    };
                    await _context.WorksWiths.AddAsync(worksWith);
                }

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                _cache.Set(request.IdempotencyKey, true, TimeSpan.FromDays(1));

                return (true, created, worksWith);
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("Unique") == true)
            {
                await dbTransaction.RollbackAsync();
                var existing = await _context.WorksWiths.FindAsync(request.EmployeeId, request.ClientId);
                return (true, false, existing);
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                return (false, false, null);
            }
        }
    }
}
