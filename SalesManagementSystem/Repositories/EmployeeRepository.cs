using Microsoft.EntityFrameworkCore;
using System.Threading;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using SalesManagementSystem.Models;
using System.Collections;

namespace SalesManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetAllEmployeeAsync(QueryObject query, CancellationToken cancellationToken = default)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Employees.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees.Include(e => e.Branch).Include(e => e.Supervisor).AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == employeeId, cancellationToken);
        }

        public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return;
        }

        public async Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateEmployee, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId ==  employeeId, cancellationToken);
            if (employee == null)
            {
                return false;
            }
            employee.ToEmployeeFromUpdateEmployeeDto(updateEmployee);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task DeleteEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return;
        }
    }
}
