using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Interfaces;
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
            List<GetBranchDto> branchDtos = branchList.Select(b => new GetBranchDto
            {
                BranchId = b.BranchId,
                BranchName = b.BranchName,
                ManagerId = b.ManagerId,
                ManagerStartDate = b.ManagerStartDate
            }).ToList();
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
            GetByIdDetailedInfoBranchDto branchDto = new GetByIdDetailedInfoBranchDto
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                ManagerStartDate = branch.ManagerStartDate
            };
            if (branch.ManagerId != null)
            {
                var manager = _employeeRepository.GetEmployeeById(branch.ManagerId.Value);
                branchDto.Manager = new GetByIdShortInfoEmployee
                {
                    EmployeeId = manager.EmployeeId,
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                };
            }
            return Ok(branchDto);
        }

        [HttpPost]
        public IActionResult CreateBranch(CreateBranchDto createBranchDto)
        {
            if(createBranchDto.ManagerId != null)
            {
                var manager = _employeeRepository.GetEmployeeById(createBranchDto.ManagerId.Value);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
            }
            var branch = new Branch
            {
                BranchName = createBranchDto.BranchName,
                ManagerId = createBranchDto.ManagerId,
                ManagerStartDate = createBranchDto.ManagerStartDate
            };

            _branchRepository.AddBranch(branch);

            return Ok(branch);
        }

        [HttpPut("{branchId:int}")]
        public IActionResult UpdateBranch(int branchId, UpdateBranchDto updateBranchDto)
        {
            var branch = _branchRepository.GetBranchById(branchId);
            if (branch == null)
            {
                return NotFound();
            }
            branch.BranchName = updateBranchDto.BranchName;
            branch.ManagerId = updateBranchDto.ManagerId;
            branch.ManagerStartDate = updateBranchDto.ManagerStartDate;

            _branchRepository.UpdateBranch(branch);

            return Ok(branch);
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
