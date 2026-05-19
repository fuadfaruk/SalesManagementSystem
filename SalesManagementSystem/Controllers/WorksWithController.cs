using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Interfaces;

// Add Dtos
// Add async functionality

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorksWithController : ControllerBase
    {
        IWorksWithRepository _worksWithRepository;
        public WorksWithController(IWorksWithRepository worksWithRepository)
        {
            _worksWithRepository = worksWithRepository;
        }
        [HttpGet]
        public IActionResult GetAllWorksWith()
        {
            var worksWithList = _worksWithRepository.GetAllWorksWith();

            return Ok(worksWithList);
        }

        [HttpGet("employee/{employeeId:int}")]
        public IActionResult GetAllByEmployeeIdWorksWith(int employeeId)
        {
            var worksWithList = _worksWithRepository.GetAllWorksWithByEmployeeId(employeeId);

            return Ok(worksWithList);
        }

        [HttpGet("client/{clientId:int}")]
        public IActionResult GetAllByClientIdWorksWith(int clientId)
        {
            var worksWithList = _worksWithRepository.GetAllWorksWithByClientId(clientId);

            return Ok(worksWithList);
        }

        // This is more of a ledger than a information table. So this will not need any seperate add and update functionality
        // Adding a entry will check if it already exists, if it does then it will update the entry, if it doesn't then it will add a new entry

    }
}
