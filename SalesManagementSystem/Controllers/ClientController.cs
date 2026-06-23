using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;
using System.Threading;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientController : ControllerBase
    {
        IBranchRepository _branchRepository;
        IClientRepository _clientRepository;
        public ClientController(IBranchRepository branchRepository, IClientRepository clientRepository)
        {
            _branchRepository = branchRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllClients([FromQuery] QueryObject queryObject, CancellationToken cancellationToken)
        {
            var clientList = await _clientRepository.GetAllClientsAsync(queryObject, cancellationToken);
            List<GetClientDto> employeeDtos = clientList.Select(c => c.ToGetClientsDto()).ToList();

            return Ok(employeeDtos);
        }

        [HttpGet("{clientId:int}")]
        [Authorize]
        public async Task<IActionResult> GetClient(int clientId, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId, cancellationToken);
            if (client == null)
            {
                return NotFound("Client not found");
            }

            var clientDto = client.ToGetByIdClientDto();
            var branch = await _branchRepository.GetBranchByIdAsync(client.BranchId, cancellationToken);
            clientDto.Branch = branch?.ToShortInfoBranchDto();

            return Ok(clientDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddClient(CreateClientDto createClientDto, CancellationToken cancellationToken)
        {
            var client = createClientDto.ToClientFromCreateClientDto();
            var branch = await _branchRepository.GetBranchByIdAsync(client.BranchId, cancellationToken);
            if (branch == null)
            {
                return BadRequest("Branch not found. Enter a valid branch!");
            }
            await _clientRepository.AddClientAsync(client, cancellationToken);

            return CreatedAtAction(nameof(GetClient), new { clientId = client.ClientId }, null);
        }

        [HttpPut("{clientId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateClient(int clientId, UpdateClientDto updateClientDto, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId, cancellationToken);
            if (client == null)
            {
                return NotFound("Client not found");
            }
            var branch = await _branchRepository.GetBranchByIdAsync(updateClientDto.BranchId, cancellationToken);
            if (branch == null)
            {
                return BadRequest("Branch not found. Enter a valid branch!");
            }

            await _clientRepository.UpdateClientAsync(clientId, updateClientDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{clientId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteClient(int clientId, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId, cancellationToken);
            if (client == null)
            {
                return NotFound("Client not found");
            }

            await _clientRepository.DeleteClientAsync(client, cancellationToken);

            return NoContent();
        }
    }
}

