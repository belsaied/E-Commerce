using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.ProductModule;
using Shared.ErrorModels;
namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager):ApiController
    {
        [RedisCache]
        // EndPoint => GetAllProducts      
        [HttpGet]  //BaseUrl/Products
        // Http Status Code 200 (success)
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationParameters parameters)
            =>  Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));
        // EndPoint => GetAllBrands
        [HttpGet("Brands")]                  //BaseUrl/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());
        // EndPoint => GetAllTypes.
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());

        [ProducesResponseType(typeof(ProductResultDto),StatusCodes.Status200OK)]
        // EndPoint => GetProductsById
        [HttpGet("{id:int}")]         ////BaseUrl/Products/{id}
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetByIdAsync(id));
    }
}
