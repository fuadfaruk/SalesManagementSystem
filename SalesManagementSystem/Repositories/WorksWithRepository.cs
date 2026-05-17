using SalesManagementSystem.Data;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Repositories
{
    public class WorksWithRepository : IWorksWithRepository
    {
        private readonly ApplicationDbContext _context;
        public WorksWithRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WorksWith> GetAllWorksWith()
        {
            return _context.WorksWiths.ToList();
        }

        public List<WorksWith> GetAllWorksWithByEmployeeId(int employeeId)
        {
            return _context.WorksWiths.Where(ww => ww.EmployeeId == employeeId).ToList();
        }

        public List<WorksWith> GetAllWorksWithByClientId(int clientId)
        {
            return _context.WorksWiths.Where(ww => ww.ClientId == clientId).ToList();
        }

        public WorksWith? GetByIdWorksWith(int employeeId, int clientId)
        {
            return _context.WorksWiths.FirstOrDefault(ww => ww.EmployeeId == employeeId && ww.ClientId == clientId);
        }

        public void AddWorksWith(WorksWith worksWith)
        {
            _context.WorksWiths.Add(worksWith);
            _context.SaveChanges();

            return;
        }

        public void UpdateWorksWith(WorksWith worksWith)
        {
            _context.WorksWiths.Update(worksWith);
            _context.SaveChanges();

            return;
        }

        public void DeleteWorksWith(WorksWith worksWith)
        {
            _context.WorksWiths.Remove(worksWith);
            _context.SaveChanges();

            return;
        }
    }
}
