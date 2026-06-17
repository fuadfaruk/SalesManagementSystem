using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Dtos.WorksWithDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public async Task<IActionResult> GetAllWorksWiths()
        {
            var worksWithList = await _worksWithRepository.GetAllWorksWithAsync();
            List<GetWorksWithDto> worksWithDtos = worksWithList.Select(w => w.ToGetWorksWithDto()).ToList();

            return Ok(worksWithDtos);
        }

        [HttpGet("employee/{employeeId:int}")]
        public async Task<IActionResult> GetAllByEmployeeIdWorksWiths(int employeeId)
        {
            var worksWithList = await _worksWithRepository.GetAllWorksWithByEmployeeIdAsync(employeeId);
            List<GetWorksWithDto> worksWithDtos = worksWithList.Select(w => w.ToGetWorksWithDto()).ToList();

            return Ok(worksWithDtos);
        }

        [HttpGet("client/{clientId:int}")]
        public async Task<IActionResult> GetAllByClientIdWorksWiths(int clientId)
        {
            var worksWithList = await _worksWithRepository.GetAllWorksWithByClientIdAsync(clientId);
            List<GetWorksWithDto> worksWithDtos = worksWithList.Select(w => w.ToGetWorksWithDto()).ToList();

            return Ok(worksWithDtos);
        }

        [HttpGet("{employeeId:int}/{clientId:int}")]
        public async Task<IActionResult> GetWorksWith(int employeeId, int clientId)
        {
            var worksWith = await _worksWithRepository.GetByIdWorksWithAsync(employeeId, clientId);
            if (worksWith == null)
            {
                return NotFound("WorksWith entry not found.");
            }

            return Ok(worksWith.ToGetWorksWithDto());
        }

        [HttpPost]
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

            var result = await _worksWithRepository.ProcessTransactionAsync(transactionRequestDto);

            if (!result.Success)
            {
                return BadRequest("Transaction entry was unsuccessful! Try again!");
            }

            if (result.Created && result.Entity != null)
            {
                return CreatedAtAction(nameof(GetWorksWith),
                    new { employeeId = result.Entity.EmployeeId, clientId = result.Entity.ClientId },
                    result.Entity.ToGetWorksWithDto());
            }

            return Ok("Transaction was added/updated successfully!");
        }

    }
}
