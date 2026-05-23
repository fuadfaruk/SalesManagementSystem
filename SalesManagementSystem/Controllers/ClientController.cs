using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;

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
            List<GetAllClientDto> employeeDtos = clientList.Select(c => c.ToGetAllClientsDto()).ToList();

            return Ok(employeeDtos);
        }

        [HttpGet("{clientId:int}")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            var client = _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                return BadRequest("Client not found");
            }

            var clientDto = client.ToGetByIdClientDto();
            clientDto.Branch = await _branchRepository.GetBranchByIdAsync(client.BranchId);

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(CreateClientDto createClientDto)
        {
            var client = createClientDto.ToClientFromCreateClientDto();
            var branch = await _branchRepository.GetBranchByIdAsync(client.BranchId);
            if (branch == null)
            {
                return BadRequest("Branch not found. Enter a valid branch!");
            }
            _clientRepository.AddClient(client);

            return Ok(client);
        }

        [HttpPut("{clientId:int}")]
        public async Task<IActionResult> UpdateClient(int clientId, UpdateClientDto updateClientDto)
        {
            var client = _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                return NotFound("Client not found");
            }
            var branch = await _branchRepository.GetBranchByIdAsync(updateClientDto.BranchId);
            if (branch == null)
            {
                return BadRequest("Branch not found. Enter a valid branch!");
            }

            client.ToClientFromUpdateClientDto(updateClientDto);

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

