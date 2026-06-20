using SalesManagementSystem.Dtos.BranchDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Mapper
{
    public static class BranchMapper
    {
        public static GetBranchDto ToGetBranchDto(this Branch branch)
        {
            return new GetBranchDto
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                ManagerId = branch.ManagerId,
                ManagerStartDate = branch.ManagerStartDate
            };
        }

        public static DetailedInfoBranchDto ToGetByIdDetailedInfoBranchDto(this Branch branch)
        {
            return new DetailedInfoBranchDto
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                ManagerStartDate = branch.ManagerStartDate
            };
        }

        public static ShortInfoBranchDto ToShortInfoBranchDto(this Branch branch)
        {
            return new ShortInfoBranchDto
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                ManagerId = branch.ManagerId
            };
        }

        public static Branch ToBranchFromCreateBranchDto(this CreateBranchDto createBranceDto)
        {
            return new Branch
            {
                BranchName = createBranceDto.BranchName,
                ManagerId = createBranceDto.ManagerId,
            };
        }

        public static void ToBranchFromUpdateBranchDto(this Branch branch, UpdateBranchDto updateBranchDto)
        {
            branch.BranchName = updateBranchDto.BranchName;
            branch.ManagerId = updateBranchDto.ManagerId;
            branch.ManagerStartDate = updateBranchDto.ManagerStartDate;

            return;
        }
    }
}
