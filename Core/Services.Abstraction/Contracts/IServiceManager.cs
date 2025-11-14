
namespace Services.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService basketService { get; }
        public IAuthenticationService AuthenticationService { get;}
        public IOrderService OrderService { get; }
        public IPaymentService PaymentService { get; }
    }
}
