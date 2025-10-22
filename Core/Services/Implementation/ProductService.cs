using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Shared.Dtos;

namespace Services.Implementation
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            //1] take instance from IUoW to reach the GenericRepo and get the method GetAllAsync() from it and then map the IEnumerable<ProductBrand> to BrandResultDto.
            var brands = await _unitOfWork.GetReopsitory<ProductBrand,int>().GetAllAsync();
            //2] Map from IEnumerable<ProductBrand> to BrandResultDto using auto Mapper
            var brandsResult =  _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandsResult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetReopsitory<Product,int>().GetAllAsync();
            var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            return productsResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetReopsitory<ProductType,int>().GetAllAsync();
            var typesResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesResult;
        }

        public async Task<ProductResultDto> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.GetReopsitory<Product, int>().GetByIdAsync(id);
            var productResult = _mapper.Map<ProductResultDto>(product);   // we don't return IEnumerable .
            return productResult;
        }
    }
}
