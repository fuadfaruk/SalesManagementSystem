using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;

// Add async functionality

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
        public async Task<IActionResult> GetAllEmployee()
        {
            var employeeList = await _employeeRepository.GetAllEmployeeAsync();
            List<GetAllEmployeeDto> employeeDtos = employeeList.Select(s => s.ToGetAllEmployeeDto()).ToList();

            return Ok(employeeDtos);
        }

        [HttpGet("{empId:int}")]
        public async Task<IActionResult> GetEmployeeById(int empId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
            if (employee == null) 
            {
                return NotFound();
            }
            GetByIdDetailedInfoEmployeeDto employeeDto = employee.ToGetByIdDetailedInfoEmployeeDto();
            if (employee.BranchId != null)
            {
                var branch = await _branchRepository.GetBranchByIdAsync(employee.BranchId.Value);
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
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto employeeDto)
        {
            if (employeeDto.SuperId != null)
            {
                var supervisor = await _employeeRepository.GetEmployeeByIdAsync(employeeDto.SuperId.Value);
                if(supervisor == null)
                {
                    return NotFound("Supervisor ID does not exist!");
                }
            }
            if(employeeDto.BranchId != null)
            {
                var branch = await _branchRepository.GetBranchByIdAsync(employeeDto.BranchId.Value);
                if (branch == null)
                {
                    return NotFound("Branch ID does not exist!");
                }
            }
            var employee = employeeDto.ToEmployeeFromCreateEmployeeDto();
            await _employeeRepository.AddEmployeeAsync(employee);

            return Ok(employee);
        }

        [HttpPut("{empId:int}")]
        public async Task<IActionResult> UpdateEmployee(int empId, UpdateEmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
            if (employee == null)
            {
                return NotFound("Employee ID does not exist!");
            }
            if(employeeDto.BranchId != null && employeeDto.BranchId != employee.BranchId)
            {
                var branch = await _branchRepository.GetBranchByIdAsync(employeeDto.BranchId.Value);
                if (branch == null)
                {
                    return NotFound("Branch ID does not exist!");
                }
            }
            if(employeeDto.SuperId != null && employeeDto.SuperId != employee.SupervisorId)
            {
                var supervisor = await _employeeRepository.GetEmployeeByIdAsync(employeeDto.SuperId.Value);
                if (supervisor == null)
                {
                    return NotFound("Supervisor ID does not exist!");
                }
            }

            var updated = await _employeeRepository.UpdateEmployeeAsync(empId, employeeDto);
            if (updated == false)
            {
                return BadRequest("An error occured!");
            }

            return Ok(employee);
        }

        [HttpDelete("{empId:int}")]
        public async Task<IActionResult> DeleteEmployee(int empId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
            if (employee == null)
            {
                return NotFound("Employee ID does not exist!");
            }
            if (employee.BranchId != null)
            {
                var branch = await _branchRepository.GetBranchByIdAsync(employee.BranchId.Value);
                if (branch != null)
                {
                    return BadRequest("Cannot delete employee because they are a manager of a branch. Please assign another manager to the branch before deleting this employee.");
                }
            }

            await _employeeRepository.DeleteEmployeeAsync(employee);

            return NoContent();
        }
    }
}
