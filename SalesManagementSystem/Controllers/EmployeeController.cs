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
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                SupervisorId = e.SupervisorId,
                BranchId = e.BranchId
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
                EmployeeId = employee.EmployeeId,
                BirthDate = employee.BirthDay,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                Sex = employee.Sex,
                SuperId = employee.SupervisorId,
            };
            if (employee.BranchId != null)
            {
                var branch = _branchRepository.GetBranchById(employee.BranchId.Value);
                if (branch != null)
                {
                    employeeDto.Branch = new GetByIdShortInfoBranchDto
                    {
                        BranchId = branch.BranchId,
                        BranchName = branch.BranchName,
                        MangerId = branch.ManagerId
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
                BirthDay = employeeDto.BirthDate,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Salary = employeeDto.Salary,
                Sex = employeeDto.Sex,
                SupervisorId = employeeDto.SuperId,
                BranchId = employeeDto.BranchId
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
            if(employeeDto.BranchId != null && employeeDto.BranchId != employee.BranchId)
            {
                var branch = _branchRepository.GetBranchById(employeeDto.BranchId.Value);
                if (branch == null)
                {
                    return NotFound("Branch ID does not exist!");
                }
            }

            employee.BirthDay = employeeDto.BirthDate;
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Salary = employeeDto.Salary;
            employee.Sex = employeeDto.Sex;
            employee.SupervisorId = employeeDto.SuperId;
            employee.BranchId = employeeDto.BranchId;

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
