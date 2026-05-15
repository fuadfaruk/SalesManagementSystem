using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
using SalesManagementSystem.Dtos.BranchDtos;

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
                branchDto.Manager = _context.Employees.FirstOrDefault(e => e.emp_id == branch.mgr_id);
            }
            return Ok(branchDto);
        }
    }
}
