using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Dtos.ClientDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Mapper
{
    public static class ClientMapper
    {
        public static GetAllClientDto ToGetAllClientsDto(this Client c)
        {
            return new GetAllClientDto
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                BranchId = c.BranchId,
            };
        }

        public static GetByIdClientDto ToGetByIdClientDto(this Client c)
        {
            return new GetByIdClientDto
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                BranchId = c.BranchId
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
