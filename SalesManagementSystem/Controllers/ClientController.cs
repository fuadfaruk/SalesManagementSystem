using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        IBranchRepository _branchRepository;
        IClientRepository _clientRepository;
        public ClientController(IBranchRepository branchRepository, IClientRepository clientRepository)
        {
            _branchRepository = branchRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult GetAllClient()
        {
            var clientList = _clientRepository.GetAllClients();
            List<GetAllClientDto> employeeDtos = clientList.Select(c => new GetAllClientDto
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                BranchId = c.BranchId,
            }).ToList();

            return Ok(employeeDtos);
        }

        [HttpGet("{clientId:int}")]
        public IActionResult GetClient(int clientId)
        {
            var client = _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                return BadRequest("Client not found");
            }

            var clientDto = new GetByIdClientDto
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                BranchId = client.BranchId,
                Branch = client.Branch
            };

            return Ok(client);
        }

        [HttpPost]
        public IActionResult AddClient(CreateClientDto createClientDto)
        {
            var client = new Client
            {
                ClientName = createClientDto.ClientName,
                BranchId = createClientDto.BranchId,
            };

            var branch = _branchRepository.GetBranchById(client.BranchId);
            if (branch == null)
            {
                return BadRequest("Branch not found. Enter a valid branch!");
            }
            _clientRepository.AddClient(client);

            return Ok(client);
        }

        [HttpPut("{clientId:int}")]
        public IActionResult UpdateClient(int clientId, UpdateClientDto updateClientDto)
        {
            var client = _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                return NotFound("Client not found");
            }
            var branch = _branchRepository.GetBranchById(updateClientDto.BranchId);
            if (branch == null)
            {
                return BadRequest("Branch not found. Enter a valid branch!");
            }

            client.ClientName = updateClientDto.ClientName;
            client.BranchId = updateClientDto.BranchId;

            _clientRepository.UpdateClient(client);

            return Ok(client);
        }

        [HttpDelete("{clientId:int}")]
        public IActionResult DeleteClient(int clientId)
        {
            var client = _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                return NotFound("Client not found");
            }

            _clientRepository.DeleteClient(client);

            return Ok("Client deleted successfully");
        }
    }
}

