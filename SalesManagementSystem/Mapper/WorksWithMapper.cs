using SalesManagementSystem.Dtos.WorksWithDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Mapper
{
    public static class WorksWithMapper
    {
        public static GetWorksWithDto ToGetWorksWithDto(this WorksWith worksWith)
        {
            return new GetWorksWithDto
            {
                ClientId = worksWith.ClientId,
                EmployeeId = worksWith.EmployeeId,
                Client = worksWith.Client,
                Employee = worksWith.Employee,
                TotalSales = worksWith.TotalSales
            };
        }
    }
}
