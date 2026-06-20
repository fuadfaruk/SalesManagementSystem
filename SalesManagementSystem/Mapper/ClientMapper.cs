using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Mapper
{
    public static class ClientMapper
    {
        public static GetClientDto ToGetClientsDto(this Client client)
        {
            return new GetClientDto
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                BranchId = client.BranchId,
            };
        }

        public static GetByIdClientDto ToGetByIdClientDto(this Client client)
        {
            return new GetByIdClientDto
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                BranchId = client.BranchId
            };
        }

        public static Client ToClientFromCreateClientDto(this CreateClientDto createClientDto)
        {
            return new Client
            {
                ClientName = createClientDto.ClientName,
                BranchId = createClientDto.BranchId,
            };
        }

        public static void ToClientFromUpdateClientDto(this Client client, UpdateClientDto updateClientDto)
        {
            client.ClientName = updateClientDto.ClientName;
            client.BranchId = updateClientDto.BranchId;

            return;
        }
    }
}
