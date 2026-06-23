using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using System.Threading;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchRepository _branchRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IBranchRepository branchRepository)
        {
            _employeeRepository = employeeRepository;
            _branchRepository = branchRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllEmployees([FromQuery] QueryObject query, CancellationToken cancellationToken)
        {
            var employeeList = await _employeeRepository.GetAllEmployeeAsync(query, cancellationToken);
            List<GetEmployeeDto> employeeDtos = employeeList.Select(s => s.ToGetAllEmployeeDto()).ToList();

            return Ok(employeeDtos);
        }

        [HttpGet("{employeeId:int}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeById(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId, cancellationToken);
            if (employee == null) 
            {
                return NotFound("Employee ID does not exist!");
            }
            DetailedInfoEmployeeDto employeeDto = employee.ToDetailedInfoEmployeeDto();

            return Ok(employeeDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto employeeDto)
        {
            if (employeeDto.SuperId != null)
            {
                var supervisor = await _employeeRepository.GetEmployeeByIdAsync(employeeDto.SuperId.Value);
                if (supervisor == null)
                {
                    return BadRequest("Supervisor ID does not exist!");
                }
            }
            if (employeeDto.BranchId != null)
            {
                var branch = await _branchRepository.GetBranchByIdAsync(employeeDto.BranchId.Value);
                if (branch == null)
                {
                    return BadRequest("Branch ID does not exist!");
                }
            }
            var employee = employeeDto.ToEmployeeFromCreateEmployeeDto();
            await _employeeRepository.AddEmployeeAsync(employee);

            var createdEmployeeDto = employee.ToDetailedInfoEmployeeDto();
            if (employee.BranchId != null)
            {
                var branch = await _branchRepository.GetBranchByIdAsync(employee.BranchId.Value);
                if (branch != null)
                {
                    createdEmployeeDto.Branch = branch.ToShortInfoBranchDto();
                }
            }
            
            return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = employee.EmployeeId }, createdEmployeeDto);
        }

        [HttpPut("{employeeId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateEmployee(int employeeId, UpdateEmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                return BadRequest("Employee ID does not exist!");
            }
            if(employeeDto.BranchId != null && employeeDto.BranchId != employee.BranchId)
            {
                var branch = await _branchRepository.GetBranchByIdAsync(employeeDto.BranchId.Value);
                if (branch == null)
                {
                    return BadRequest("Branch ID does not exist!");
                }
            }
            if(employeeDto.SuperId != null && employeeDto.SuperId != employee.SupervisorId)
            {
                var supervisor = await _employeeRepository.GetEmployeeByIdAsync(employeeDto.SuperId.Value);
                if (supervisor == null)
                {
                    return BadRequest("Supervisor ID does not exist!");
                }
            }

            var updated = await _employeeRepository.UpdateEmployeeAsync(employeeId, employeeDto);
            if (updated == false)
            {
                return Problem(
                    detail: "An error occurred while updating the employee in the database.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Failed to update employee"
                );
            }

            return NoContent();
        }

        [HttpDelete("{employeeId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                return BadRequest("Employee ID does not exist!");
            }
            var branch = await _branchRepository.GetBranchByManagerIdAsync(employee.EmployeeId);
            if (branch != null)
            {
                return BadRequest("Cannot delete employee because they are a manager of a branch. Please assign another manager to the branch before deleting this employee.");
            }

            await _employeeRepository.DeleteEmployeeAsync(employee);

            return NoContent();
        }
    }
}
