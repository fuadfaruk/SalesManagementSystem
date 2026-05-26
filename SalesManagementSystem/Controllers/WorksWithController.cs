using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Dtos.WorksWithDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

// Add Dtos
// Add async functionality

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorksWithController : ControllerBase
    {
        IWorksWithRepository _worksWithRepository;
        IEmployeeRepository _employeeRepository;
        IClientRepository _clientRepository;
        public WorksWithController(IWorksWithRepository worksWithRepository, IEmployeeRepository employeeRepository, IClientRepository clientRepository)
        {
            _worksWithRepository = worksWithRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWorksWith()
        {
            var worksWithList = await _worksWithRepository.GetAllWorksWithAsync();

            return Ok(worksWithList);
        }

        [HttpGet("employee/{employeeId:int}")]
        public async Task<IActionResult> GetAllByEmployeeIdWorksWith(int employeeId)
        {
            var worksWithList = await _worksWithRepository.GetAllWorksWithByEmployeeIdAsync(employeeId);

            return Ok(worksWithList);
        }

        [HttpGet("client/{clientId:int}")]
        public async Task<IActionResult> GetAllByClientIdWorksWith(int clientId)
        {
            var worksWithList = await _worksWithRepository.GetAllWorksWithByClientIdAsync(clientId);

            return Ok(worksWithList);
        }

        // This is more of a ledger than a information table. So this will not need any seperate add and update functionality
        // Adding a entry will check if it already exists, if it does then it will update the entry, if it doesn't then it will add a new entry
        [HttpPut]
        public async Task<IActionResult> AddTransactionWorksWith(TransactionRequestDto transactionRequestDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(transactionRequestDto.EmployeeId);
            if (employee == null)
            {
                return BadRequest("Employee with the given id does not exist! Try again!");
            }
            var client = await _clientRepository.GetClientByIdAsync(transactionRequestDto.ClientId);
            if (client == null)
            {
                return BadRequest("Client with the given id does not exist! Try again!");
            }
            var recorded = await _worksWithRepository.ProcessTransactionAsync(transactionRequestDto);

            if(recorded == false)
            {
                return BadRequest("Transaction entry was unsuccessful! Try again!");
            }
            return Ok("Transaction was added successfully!");
        }
    }
}
