using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Models;

// Add Dto for GetById
// Fix other Dtos after adding brach functionality
// Add asysc functionality
// Add repository pattern
// Add automapper

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
        public IActionResult GetAllEmployee() // Use Dto
        {
            var employeeList = _context.Employees.ToList();
            var employeeDtoList = employeeList.Select(e => new GetAllEmployeeDto
            {
                emp_id = e.emp_id,
                first_name = e.first_name,
                last_name = e.last_name,
                super_id = e.super_id,
                branch_id = e.branch_id
            }).ToList();
            return Ok(employeeDtoList);
        }

        [HttpGet("{empId:int}")]
        public IActionResult GetEmployeeById(int empId) // Use Dto
        {
            var employee = _context.Employees.FirstOrDefault(e => e.emp_id == empId); // Put this line in repository class
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
                sex = employeeDto.sex,
                super_id = employeeDto.super_id,
                branch_id = employeeDto.branch_id
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
            employee.sex = employeeDto.sex;
            employee.super_id = employeeDto.super_id;
            employee.branch_id = employeeDto.branch_id;

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
