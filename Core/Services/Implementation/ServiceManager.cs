
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementation
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper ,IBasketRepository _basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IProductService ProductService => _productService.Value;

        public IBasketService basketService => _basketService.Value;
    }
}
