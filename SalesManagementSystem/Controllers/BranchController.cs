using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
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
                branch_id = b.BranchId,
                branch_name = b.BranchName,
                mgr_id = b.ManagerId,
                mgr_start_date = b.ManagerStartDate
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
                branch_id = branch.BranchId,
                branch_name = branch.BranchName,
                mgr_start_date = branch.ManagerStartDate
            };
            if (branch.ManagerId != null)
            {
                var manager = _employeeRepository.GetEmployeeById(branch.ManagerId.Value);
                branchDto.Manager = new GetByIdShortInfoEmployee
                {
                    emp_id = manager.EmployeeId,
                    first_name = manager.FirstName,
                    last_name = manager.LastName,
                };
            }
            return Ok(branchDto);
        }

        [HttpPost]
        public IActionResult CreateBranch(CreateBranchDto createBranchDto)
        {
            if(createBranchDto.mgr_id != null)
            {
                var manager = _employeeRepository.GetEmployeeById(createBranchDto.mgr_id.Value);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
            }
            var branch = new Branch
            {
                BranchName = createBranchDto.branch_name,
                ManagerId = createBranchDto.mgr_id,
                ManagerStartDate = createBranchDto.mgr_start_date
            };

            _branchRepository.AddBranch(branch);

            return Ok();
        }

        [HttpPut("{branchId:int}")]
        public IActionResult UpdateBranch(int branchId, UpdateBranchDto updateBranchDto)
        {
            var branch = _branchRepository.GetBranchById(branchId);
            if (branch == null)
            {
                return NotFound();
            }
            branch.BranchName = updateBranchDto.branch_name;
            branch.ManagerId = updateBranchDto.mgr_id;
            branch.ManagerStartDate = updateBranchDto.mgr_start_date;

            _branchRepository.UpdateBranch(branch);

            return Ok();
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

            return Ok();
        }
    }
}
