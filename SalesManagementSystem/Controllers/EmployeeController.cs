using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var employeeList = _context.Employees.ToList();
            return Ok(employeeList);
        }

        [HttpGet("{empId:int}")]
        public IActionResult GetEmployeeById(int empId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.emp_id == empId);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public IActionResult AddEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                birth_date = employeeDto.birth_date,
                first_name = employeeDto.first_name,
                last_name = employeeDto.last_name,
                salary = employeeDto.salary,
                sex = employeeDto.sex
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployeeById), new { empId = employee.emp_id }, employee);
        }

        [HttpPut("{empId:int}")]
        public IActionResult UpdateEmployee(int empId, UpdateEmployeeDto employeeDto)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.emp_id == empId);
            if (employee == null)
            {
                return NotFound();
            }
            employee.birth_date = employeeDto.birth_date;
            employee.first_name = employeeDto.first_name;
            employee.last_name = employeeDto.last_name;
            employee.salary = employeeDto.salary;

            _context.SaveChanges();
            return CreatedAtAction(nameof(UpdateEmployee), new { empId = employee.emp_id }, employee);
        }

        [HttpDelete("{empId:int}")]
        public IActionResult DeleteEmployee(int empId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.emp_id == empId);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
