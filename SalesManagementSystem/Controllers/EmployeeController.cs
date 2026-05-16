using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

// Add repository pattern
// Add async functionality
// Add automapper

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, ApplicationDbContext context)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var employeeList = _employeeRepository.GetAllEmployees();
            List<GetAllEmployeeDto> employeeDtos = employeeList.Select(e => new GetAllEmployeeDto // Put this line in a mapper class
            {
                emp_id = e.emp_id,
                first_name = e.first_name,
                last_name = e.last_name,
                super_id = e.super_id,
                branch_id = e.branch_id
            }).ToList();
            return Ok(employeeDtos);
        }

        [HttpGet("{empId:int}")]
        public IActionResult GetEmployeeById(int empId)
        {
            var employee = _employeeRepository.GetEmployeeById(empId);
            if (employee == null) 
            {
                return NotFound();
            }
            GetByIdDetailedInfoEmployeeDto employeeDto = new GetByIdDetailedInfoEmployeeDto
            {
                emp_id = employee.emp_id,
                birth_date = employee.birth_date,
                first_name = employee.first_name,
                last_name = employee.last_name,
                salary = employee.salary,
                sex = employee.sex,
                super_id = employee.super_id,
            };
            if (employee.branch_id != null)
            {
                var branch = _context.Branches.FirstOrDefault(b => b.branch_id == employee.branch_id);
                if (branch != null)
                {
                    employeeDto.Branch = new GetByIdShortInfoBranchDto
                    {
                        branch_id = branch.branch_id,
                        branch_name = branch.branch_name,
                        mgr_id = branch.mgr_id
                    };
                }
            }
            return Ok(employeeDto);
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
            _employeeRepository.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { empId = employee.emp_id }, employee);
        }

        [HttpPut("{empId:int}")]
        public IActionResult UpdateEmployee(int empId, UpdateEmployeeDto employeeDto)
        {
            var employee = _employeeRepository.GetEmployeeById(empId);
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

            // Add checkup for the existance of branch_id

            _employeeRepository.UpdateEmployee(employee);
            return CreatedAtAction(nameof(UpdateEmployee), new { empId = employee.emp_id }, employee);
        }

        [HttpDelete("{empId:int}")]
        public IActionResult DeleteEmployee(int empId)
        {
            // Deleting a employee that is a manager of a branch throws an error, fix it!
            var employee = _employeeRepository.GetEmployeeById(empId);
            if (employee == null)
            {
                return NotFound("ID does not exists!");
            }

            _employeeRepository.DeleteEmployee(employee);
            return NoContent();
        }
    }
}
