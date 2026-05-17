using SalesManagementSystem.Data;
using SalesManagementSystem.Interfaces;
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

        public List<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

        public Employee? GetEmployeeById(int empId)
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeId == empId);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return;
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();

            return;
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return;
        }
    }
}
