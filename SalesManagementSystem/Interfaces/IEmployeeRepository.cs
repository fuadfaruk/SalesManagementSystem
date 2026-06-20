using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeeAsync(QueryObject query);
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateEmployee);
        Task DeleteEmployeeAsync(Employee employee);
    }
}
