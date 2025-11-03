using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IProductService
    {
        //GetAllProducts
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        //GetAllBrands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //GetAllTypes
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        //GetProductById
        Task<ProductResultDto> GetByIdAsync(int id);
    }
}
