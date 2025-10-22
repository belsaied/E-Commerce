using Shared.Dtos;
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
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
        //GetAllBrands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //GetAllTypes
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        //GetProductById
        Task<ProductResultDto> GetByIdAsync(int id);
    }
}
