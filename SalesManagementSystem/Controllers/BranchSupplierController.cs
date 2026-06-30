using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.BranchSupplierDtos;
using SalesManagementSystem.Helpers;
using SalesManagementSystem.Interfaces;
using SalesManagementSystem.Mapper;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BranchSupplierController : ControllerBase
    {
        private readonly IBranchSupplierRepository _branchSupplierRepository;
        public BranchSupplierController(IBranchSupplierRepository branchSupplierRepository)
        {
            _branchSupplierRepository = branchSupplierRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBranchSuppliers([FromQuery] QueryObject queryObject, CancellationToken cancellationToken)
        {
            var branchSuppliers = await _branchSupplierRepository.GetAllBranchSuppliersAsync(queryObject, cancellationToken);
            List<GetBranchSupplierDto> branchSupplierDtos = branchSuppliers.Select(b => b.ToGetAllBranchSupplierDto()).ToList();

            return Ok(branchSupplierDtos);
        }
        // Add authorization
        // Add get by id
        // Add create
        // Add update
        // Add delete
    }
}
