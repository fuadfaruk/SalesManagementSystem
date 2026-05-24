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
        public async Task<IActionResult> GetAllClient()
        {
            var clientList = await _clientRepository.GetAllClientsAsync();
            List<GetAllClientDto> employeeDtos = clientList.Select(c => c.ToGetAllClientsDto()).ToList();

            return Ok(employeeDtos);
        }

        [HttpGet("{clientId:int}")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId);
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
            await _clientRepository.AddClientAsync(client);

            return Ok(client);
        }

        [HttpPut("{clientId:int}")]
        public async Task<IActionResult> UpdateClient(int clientId, UpdateClientDto updateClientDto)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId);
            if (client == null)
            {
                return NotFound("Client not found");
            }
            var branch = await _branchRepository.GetBranchByIdAsync(updateClientDto.BranchId);
            if (branch == null)
            {
                return BadRequest("Branch not found. Enter a valid branch!");
            }

            await _clientRepository.UpdateClientAsync(clientId, updateClientDto);

            return Ok(client);
        }

        [HttpDelete("{clientId:int}")]
        public async Task<IActionResult> DeleteClient(int clientId)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId);
            if (client == null)
            {
                return NotFound("Client not found");
            }

            await _clientRepository.DeleteClientAsync(client);

            return Ok("Client deleted successfully");
        }
    }
}

