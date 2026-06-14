using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> GetAllBranches()
        {
            var branchList = await _branchRepository.GetAllBranchAsync();
            List<GetBranchDto> branchDtos = branchList.Select(b => b.ToGetBranchDto()).ToList();

            return Ok(branchDtos);
        }

        [HttpGet("{branchId:int}")]
        public async Task<IActionResult> GetBranchById(int branchId)
        {
            var branch = await _branchRepository.GetBranchByIdAsync(branchId);
            if (branch == null)
            {
                return NotFound();
            }

            GetByIdDetailedInfoBranchDto branchDto = branch.ToGetByIdDetailedInfoBranchDto();
            if (branch.ManagerId != null)
            {
                var manager = await _employeeRepository.GetEmployeeByIdAsync(branch.ManagerId.Value);
                if(manager != null)
                {
                    branchDto.Manager = manager.ToGetByIdShortInfoEmployeeDto();
                }
            }
            return Ok(branchDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(CreateBranchDto createBranchDto)
        {
            if (createBranchDto.ManagerId != null)
            {
                var manager = await _employeeRepository.GetEmployeeByIdAsync(createBranchDto.ManagerId.Value);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
                // This should be changed as an employee can be a manager of multiple branches, but for now I will keep it as is.
                var existingBranch = await _branchRepository.GetBranchByManagerIdAsync(manager.EmployeeId);
                if(existingBranch != null)
                {
                    return BadRequest("The specified manager is already assigned to another branch.");
                }
            }
            var branch = createBranchDto.ToBranchFromCreateBranchDto();
            await _branchRepository.AddBranchAsync(branch);

            return Ok(branch.ToGetByIdDetailedInfoBranchDto());
        }

        [HttpPut("{branchId:int}")]
        public async Task<IActionResult> UpdateBranch(int branchId, UpdateBranchDto updateBranchDto)
        {
            var branch = await _branchRepository.GetBranchByIdAsync(branchId);
            if (branch == null)
            {
                return NotFound("Branch ID does not exists!");
            }
            if (updateBranchDto.ManagerId != null)
            {
                var manager = await _employeeRepository.GetEmployeeByIdAsync(updateBranchDto.ManagerId.Value);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
                // This should be changed as an employee can be a manager of multiple branches, but for now I will keep it as is.
                var existingBranch = await _branchRepository.GetBranchByManagerIdAsync(manager.EmployeeId);
                if (existingBranch != null)
                {
                    return BadRequest("The specified manager is already assigned to another branch.");
                }
            }
            await _branchRepository.UpdateBranchAsync(branchId, updateBranchDto);

            return Ok();
        }

        [HttpDelete("{branchId:int}")]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            var branch = await _branchRepository.GetBranchByIdAsync(branchId);
            if (branch == null)
            {
                return NotFound();
            }
            await _branchRepository.DeleteBranchAsync(branch);

            return NoContent();
        }
    }
}
