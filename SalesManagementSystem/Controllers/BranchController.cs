using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBranch()
        {
            var branchList = _context.Branches.ToList();
            List<GetBranchDto> branchDtos = branchList.Select(b => new GetBranchDto
            {
                branch_id = b.branch_id,
                branch_name = b.branch_name,
                mgr_id = b.mgr_id,
                mgr_start_date = b.mgr_start_date
            }).ToList();
            return Ok(branchDtos);
        }

        [HttpGet("{branchId:int}")]
        public IActionResult GetBranchById(int branchId)
        {
            var branch = _context.Branches.FirstOrDefault(b => b.branch_id == branchId);
            if (branch == null)
            {
                return NotFound();
            }
            GetByIdDetailedInfoBranchDto branchDto = new GetByIdDetailedInfoBranchDto
            {
                branch_id = branch.branch_id,
                branch_name = branch.branch_name,
                mgr_start_date = branch.mgr_start_date
            };
            if (branch.mgr_id != null)
            {
                var manager = _context.Employees.FirstOrDefault(e => e.emp_id == branch.mgr_id);
                branchDto.Manager = new GetByIdShortInfoEmployee
                {
                    emp_id = manager.emp_id,
                    first_name = manager.first_name,
                    last_name = manager.last_name,
                };
            }
            return Ok(branchDto);
        }

        [HttpPost]
        public IActionResult CreateBranch(CreateBranchDto createBranchDto)
        {
            if(createBranchDto.mgr_id != null)
            {
                var manager = _context.Employees.FirstOrDefault(e => e.emp_id == createBranchDto.mgr_id);
                if (manager == null)
                {
                    return BadRequest("Manager with the specified ID does not exist.");
                }
            }
            var branch = new Branch
            {
                branch_name = createBranchDto.branch_name,
                mgr_id = createBranchDto.mgr_id,
                mgr_start_date = createBranchDto.mgr_start_date
            };

            _context.Branches.Add(branch);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{branchId:int}")]
        public IActionResult UpdateBranch(int branchId, UpdateBranchDto updateBranchDto)
        {
            var branch = _context.Branches.FirstOrDefault(b => b.branch_id == branchId);
            if (branch == null)
            {
                return NotFound();
            }
            branch.branch_name = updateBranchDto.branch_name;
            branch.mgr_id = updateBranchDto.mgr_id;
            branch.mgr_start_date = updateBranchDto.mgr_start_date;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{branchId:int}")]
        public IActionResult DeleteBranch(int branchId)
        {
            var branch = _context.Branches.FirstOrDefault(b => b.branch_id == branchId);
            if (branch == null)
            {
                return NotFound();
            }
            _context.Branches.Remove(branch);
            _context.SaveChanges();

            return Ok();
        }
    }
}
