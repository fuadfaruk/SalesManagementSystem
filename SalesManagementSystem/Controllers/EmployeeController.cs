using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

// Add supervisor validation check
// Add repository pattern
// Add async functionality
// Add automapper

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchRepository _branchRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IBranchRepository branchRepository)
        {
            _employeeRepository = employeeRepository;
            _branchRepository = branchRepository;
        }

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var employeeList = _employeeRepository.GetAllEmployee();
            List<GetAllEmployeeDto> employeeDtos = employeeList.Select(e => new GetAllEmployeeDto // Put this line in a mapper class
            {
                emp_id = e.EmployeeId,
                first_name = e.FirstName,
                last_name = e.LastName,
                super_id = e.SupervisorId,
                branch_id = e.BranchId
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
                emp_id = employee.EmployeeId,
                birth_date = employee.BirthDay,
                first_name = employee.FirstName,
                last_name = employee.LastName,
                salary = employee.Salary,
                sex = employee.Sex,
                super_id = employee.SupervisorId,
            };
            if (employee.BranchId != null)
            {
                var branch = _branchRepository.GetBranchById(employee.BranchId.Value);
                if (branch != null)
                {
                    employeeDto.Branch = new GetByIdShortInfoBranchDto
                    {
                        branch_id = branch.BranchId,
                        branch_name = branch.BranchName,
                        mgr_id = branch.ManagerId
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
                BirthDay = employeeDto.birth_date,
                FirstName = employeeDto.first_name,
                LastName = employeeDto.last_name,
                Salary = employeeDto.salary,
                Sex = employeeDto.sex,
                SupervisorId = employeeDto.super_id,
                BranchId = employeeDto.branch_id
            };
            _employeeRepository.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { empId = employee.EmployeeId }, employee);
        }

        [HttpPut("{empId:int}")]
        public IActionResult UpdateEmployee(int empId, UpdateEmployeeDto employeeDto)
        {
            var employee = _employeeRepository.GetEmployeeById(empId);
            if (employee == null)
            {
                return NotFound("Employee ID does not exist!");
            }
            if(employeeDto.branch_id != null && employeeDto.branch_id != employee.BranchId)
            {
                var branch = _branchRepository.GetBranchById(employeeDto.branch_id.Value);
                if (branch == null)
                {
                    return NotFound("Branch ID does not exist!");
                }
            }

            employee.BirthDay = employeeDto.birth_date;
            employee.FirstName = employeeDto.first_name;
            employee.LastName = employeeDto.last_name;
            employee.Salary = employeeDto.salary;
            employee.Sex = employeeDto.sex;
            employee.SupervisorId = employeeDto.super_id;
            employee.BranchId = employeeDto.branch_id;

            _employeeRepository.UpdateEmployee(employee);
            return CreatedAtAction(nameof(UpdateEmployee), new { empId = employee.EmployeeId }, employee);
        }

        [HttpDelete("{empId:int}")]
        public IActionResult DeleteEmployee(int empId)
        {
            var employee = _employeeRepository.GetEmployeeById(empId);
            if (employee == null)
            {
                return NotFound("Employee ID does not exist!");
            }
            if (employee.BranchId != null)
            {
                var branch = _branchRepository.GetBranchById(employee.BranchId.Value);
                if (branch != null)
                {
                    return BadRequest("Cannot delete employee because they are a manager of a branch. Please assign another manager to the branch before deleting this employee.");
                }
            }

            _employeeRepository.DeleteEmployee(employee);
            return NoContent();
        }
    }
}
