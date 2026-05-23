using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int empId)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == empId);
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
            employee.EmployeeId = employeeId;

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
