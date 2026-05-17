using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Interfaces
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetAllEmployee();
        public Employee? GetEmployeeById(int empId);
        public void AddEmployee(Employee employee);
        public void UpdateEmployee(Employee employee);
        public void DeleteEmployee(Employee employee);
    }
}
