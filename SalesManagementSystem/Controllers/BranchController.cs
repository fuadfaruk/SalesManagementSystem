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
    public class BranchController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchRepository _branchRepository;
        public BranchController(IEmployeeRepository employeeRepository, IBranchRepository branchRepository)
        {
            _employeeRepository = employeeRepository;
            _branchRepository = branchRepository;
        }

        [HttpGet]
        public IActionResult GetAllBranch()
        {
            var branchList = _branchRepository.GetAllBranch();
            List<GetBranchDto> branchDtos = branchList.Select(b => b.ToGetBranchDto()).ToList();

            return Ok(branchDtos);
        }

        [HttpGet("{branchId:int}")]
        public IActionResult GetBranchById(int branchId)
        {
            var branch = _branchRepository.GetBranchById(branchId);
            if (branch == null)
            {
                return NotFound();
            }

            GetByIdDetailedInfoBranchDto branchDto = branch.ToGetByIdDetailedInfoBranchDto();
            if (branch.ManagerId != null)
            {
                var manager = _employeeRepository.GetEmployeeById(branch.ManagerId.Value);
                branchDto.Manager = manager.ToGetByIdShortInfoEmployeeDto();
            }
            return Ok(branchDto);
        }

        [HttpPost]
        public IActionResult CreateBranch(CreateBranchDto createBranchDto)
        {
            if (createBranchDto.ManagerId != null)
            {
                var manager = _employeeRepository.GetEmployeeById(createBranchDto.ManagerId.Value);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
                // This should be changed as an employee can be a manager of multiple branches, but for now I will keep it as is.
                var existingBranch = _branchRepository.GetBranchByManagerId(manager.EmployeeId);
                if(existingBranch != null)
                {
                    return BadRequest("The specified manager is already assigned to another branch.");
                }
            }
            var branch = createBranchDto.ToBranchFromCreateBranchDto();
            _branchRepository.AddBranch(branch);

            return Ok(branch.ToGetByIdDetailedInfoBranchDto());
        }

        [HttpPut("{branchId:int}")]
        public IActionResult UpdateBranch(int branchId, UpdateBranchDto updateBranchDto)
        {
            var branch = _branchRepository.GetBranchById(branchId);
            if (branch == null)
            {
                return NotFound();
            }
            if (updateBranchDto.ManagerId != null)
            {
                var manager = _employeeRepository.GetEmployeeById(updateBranchDto.ManagerId.Value);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
                // This should be changed as an employee can be a manager of multiple branches, but for now I will keep it as is.
                var existingBranch = _branchRepository.GetBranchByManagerId(manager.EmployeeId);
                if (existingBranch != null)
                {
                    return BadRequest("The specified manager is already assigned to another branch.");
                }
            }
            branch.ToBranchFromUpdateBranchDto(updateBranchDto);
            _branchRepository.UpdateBranch(branch);

            return Ok(branch.ToGetByIdDetailedInfoBranchDto());
        }

        [HttpDelete("{branchId:int}")]
        public IActionResult DeleteBranch(int branchId)
        {
            var branch = _branchRepository.GetBranchById(branchId);
            if (branch == null)
            {
                return NotFound();
            }
            _branchRepository.DeleteBranch(branch);

            return NoContent();
        }
    }
}
