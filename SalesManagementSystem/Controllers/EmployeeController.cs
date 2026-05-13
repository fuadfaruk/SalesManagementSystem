using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Data;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var employeeList = _context.Employees.ToList();
            return Ok(employeeList);
        }

        [HttpGet("{empId:int}")]
        public IActionResult GetEmployeeById(int empId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.emp_id == empId);
            return employee != null ? Ok(employee) : NotFound();
        }
    }
}
