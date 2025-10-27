using Shared.Dtos;
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
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? typeId,int? brandId,ProductSortingOptions sort);
        //GetAllBrands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //GetAllTypes
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        //GetProductById
        Task<ProductResultDto> GetByIdAsync(int id);
    }
}
