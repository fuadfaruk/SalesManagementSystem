using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Employee>> GetAllEmployeeAsync(QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Employees.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees.Include(e => e.Branch).Include(e => e.Supervisor).AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateEmployee)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId ==  employeeId);
            if (employee == null)
            {
                return false;
            }
            employee.ToEmployeeFromUpdateEmployeeDto(updateEmployee);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return;
        }
    }
}
