using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared;
using Shared.Dtos;
using Shared.Enums;

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

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var productRepo = _unitOfWork.GetReopsitory<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(parameters);
            var products = await productRepo.GetAllAsync(specifications);
            var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            var pageSize = productsResult.Count();
            var countSpecifications = new ProductCountSpecifications(parameters);
            var totalCount = await productRepo.CountAsync(countSpecifications);
            return new PaginatedResult<ProductResultDto>(parameters.PageIndex, pageSize, totalCount, productsResult);

        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetReopsitory<ProductType,int>().GetAllAsync();
            var typesResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesResult;
        }

        public async Task<ProductResultDto> GetByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetReopsitory<Product, int>().GetByIdAsync(specifications);
           // we don't return IEnumerable .
            return product is null ? throw new ProductNotFoundException(id)
                : _mapper.Map<ProductResultDto>(product);
        }
    }
}
