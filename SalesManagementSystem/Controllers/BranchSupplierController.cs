using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAllBranchSuppliers([FromQuery] QueryObject queryObject, CancellationToken cancellationToken)
        {
            var branchSuppliers = await _branchSupplierRepository.GetAllBranchSuppliersAsync(queryObject, cancellationToken);
            List<GetBranchSupplierDto> branchSupplierDtos = branchSuppliers.Select(b => b.ToGetAllBranchSupplierDto()).ToList();

            return Ok(branchSupplierDtos);
        }

        [HttpGet("{branchSupplierId:int}")]
        [Authorize]
        public async Task<IActionResult> GetAllBranchSupplierById(int branchSupplierId, CancellationToken cancellationToken)
        {
            var branchSuppliers = await _branchSupplierRepository.GetAllBranchSupplierByIdAsync(branchSupplierId, cancellationToken);
            var branchSupplierDtos = branchSuppliers.Select(b => b.ToGetAllBranchSupplierDto()).ToList();

            return Ok(branchSupplierDtos);
        }

        [HttpGet("{branchSupplierId:int}/{supplierName}")]
        [Authorize]
        public async Task<IActionResult> GetBranchSupplierByIdNSupplierName(int branchSupplierId, string supplierName, CancellationToken cancellationToken)
        {
            var branchSupplier = await _branchSupplierRepository.GetBranchSupplierByIdNSupplierNameAsync(branchSupplierId, supplierName, cancellationToken);
            if (branchSupplier == null)
            {
                return NotFound("Branch Supplier ID or Supplier Name does not exist!");
            }
            var branchSupplierDto = branchSupplier.ToGetAllBranchSupplierDto();

            return Ok(branchSupplierDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBranchSupplier(CreateBranchSupplierDto branchSupplierDto, CancellationToken cancellationToken)
        {
            var branchSupplier = branchSupplierDto.ToBranchSupplier();
            await _branchSupplierRepository.AddBranchSupplierAsync(branchSupplier, cancellationToken);
            return Ok("Branch Supplier added successfully!");
        }

        [HttpPut("{branchSupplierId:int}/{supplierName}")]
        [Authorize]
        public async Task<IActionResult> UpdateBranchSupplier(int branchSupplierId, string supplierName, UpdateBranchSupplierDto branchSupplierDto, CancellationToken cancellationToken)
        {
            var existingBranchSupplier = await _branchSupplierRepository.GetBranchSupplierByIdNSupplierNameAsync(branchSupplierId, supplierName, cancellationToken);
            if (existingBranchSupplier == null)
            {
                return NotFound("Branch Supplier ID does not exist!");
            }
            var updated = await _branchSupplierRepository.UpdateBranchSupplierAsync(branchSupplierId, supplierName, branchSupplierDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{branchSupplierId:int}/{supplierName}")]
        [Authorize]
        public async Task<IActionResult> DeleteBranchSupplier(int branchSupplierId, string supplierName, CancellationToken cancellationToken)
        {
            var existingBranchSupplier = await _branchSupplierRepository.GetBranchSupplierByIdNSupplierNameAsync(branchSupplierId, supplierName, cancellationToken);
            if (existingBranchSupplier == null)
            {
                return NotFound("Branch Supplier ID does not exist!");
            }
            await _branchSupplierRepository.DeleteBranchSupplierAsync(existingBranchSupplier, cancellationToken);

            return NoContent();
        }
    }
}
