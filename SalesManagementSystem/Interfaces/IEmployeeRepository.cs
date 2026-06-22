using System.Threading;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeeAsync(QueryObject query, CancellationToken cancellationToken = default);
        Task<Employee?> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateEmployee, CancellationToken cancellationToken = default);
        Task DeleteEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}
