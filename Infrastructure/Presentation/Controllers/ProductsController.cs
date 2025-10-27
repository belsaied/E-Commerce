using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos;
using Shared.Enums;
namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager):ControllerBase
    {
        // EndPoint => GetAllProducts      
        [HttpGet]  //BaseUrl/Products
        // Http Status Code 200 (success)
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProductsAsync(int? typeId,int? brandId , ProductSortingOptions sort)
            =>  Ok(await _serviceManager.ProductService.GetAllProductsAsync(typeId,brandId,sort));
        // EndPoint => GetAllBrands
        [HttpGet("Brands")]                  //BaseUrl/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());
        // EndPoint => GetAllTypes.
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());
        // EndPoint => GetProductsById
        [HttpGet("{id:int}")]         ////BaseUrl/Products/{id}
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetByIdAsync(id));
    }
}
