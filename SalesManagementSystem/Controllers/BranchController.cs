using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using System.Threading;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchRepository _branchRepository;
        public BranchController(IEmployeeRepository employeeRepository, IBranchRepository branchRepository)
        {
            _employeeRepository = employeeRepository;
            _branchRepository = branchRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches([FromQuery] QueryObject queryObject, CancellationToken cancellationToken)
        {
            var branchList = await _branchRepository.GetAllBranchAsync(queryObject, cancellationToken);
            List<GetBranchDto> branchDtos = branchList.Select(b => b.ToGetBranchDto()).ToList();

            return Ok(branchDtos);
        }

        [HttpGet("{branchId:int}")]
        public async Task<IActionResult> GetBranchById(int branchId, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetBranchByIdAsync(branchId, cancellationToken);
            if (branch == null)
            {
                return NotFound();
            }

            DetailedInfoBranchDto branchDto = branch.ToGetByIdDetailedInfoBranchDto();
            if (branch.ManagerId != null)
            {
                var manager = await _employeeRepository.GetEmployeeByIdAsync(branch.ManagerId.Value, cancellationToken);
                if(manager != null)
                {
                    branchDto.Manager = manager.ToShortInfoEmployeeDto();
                }
            }
            return Ok(branchDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(CreateBranchDto createBranchDto, CancellationToken cancellationToken)
        {
            if (createBranchDto.ManagerId != null)
            {
                var manager = await _employeeRepository.GetEmployeeByIdAsync(createBranchDto.ManagerId.Value, cancellationToken);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
                // This should be changed as an employee can be a manager of multiple branches, but for now I will keep it as is.
                var existingBranch = await _branchRepository.GetBranchByManagerIdAsync(manager.EmployeeId, cancellationToken);
                if(existingBranch != null)
                {
                    return BadRequest("The specified manager is already assigned to another branch.");
                }
            }
            var branch = createBranchDto.ToBranchFromCreateBranchDto();
            await _branchRepository.AddBranchAsync(branch, cancellationToken);

            return CreatedAtAction(nameof(GetBranchById), new { branchId = branch.BranchId }, null);
        }

        [HttpPut("{branchId:int}")]
        public async Task<IActionResult> UpdateBranch(int branchId, UpdateBranchDto updateBranchDto, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetBranchByIdAsync(branchId, cancellationToken);
            if (branch == null)
            {
                return NotFound("Branch ID does not exists!");
            }
            if (updateBranchDto.ManagerId != null)
            {
                var manager = await _employeeRepository.GetEmployeeByIdAsync(updateBranchDto.ManagerId.Value, cancellationToken);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
                var existingBranch = await _branchRepository.GetBranchByManagerIdAsync(manager.EmployeeId, cancellationToken);
                if (existingBranch != null)
                {
                    return BadRequest("The specified manager is already assigned to another branch.");
                }
            }
            await _branchRepository.UpdateBranchAsync(branchId, updateBranchDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{branchId:int}")]
        public async Task<IActionResult> DeleteBranch(int branchId, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetBranchByIdAsync(branchId, cancellationToken);
            if (branch == null)
            {
                return NotFound();
            }
            await _branchRepository.DeleteBranchAsync(branch, cancellationToken);

            return NoContent();
        }
    }
}
