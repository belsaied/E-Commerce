using Services.Abstraction.Contracts;

namespace Services.Implementation
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> _productFactory
        , Func<IOrderService> _orderFactory, Func<IAuthenticationService> _authFactory
        , Func<IPaymentService> _paymentFactory, Func<IBasketService> _basketFactory
        , Func<ICacheService> _cacheFactory) : IServiceManager
    {
        public IProductService ProductService => _productFactory.Invoke();

        public IBasketService basketService => _basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authFactory.Invoke();

        public IOrderService OrderService => _orderFactory.Invoke();

        public IPaymentService PaymentService => _paymentFactory.Invoke();

        public ICacheService CacheService => _cacheFactory.Invoke();
    }
}
