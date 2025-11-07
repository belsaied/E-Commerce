
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts;
using Shared.Common;

namespace Services.Implementation
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper 
        ,IBasketRepository _basketRepository,UserManager<User> _userManager
        ,IOptions<JwtOptions> _options) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _authService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager,_options));
        public IProductService ProductService => _productService.Value;

        public IBasketService basketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authService.Value;
    }
}
