using Shared.Dtos.OrderModule;

namespace Services.Abstraction.Contracts
{
    public interface IOrderService
    {
        // GetById ==> Take Guid Id ==> Return OrderResult
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        // GetAllByEmail ==> Take string Email ==> Return IEnummerable <OrderResult>
        Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string userEmail);
        // CreateOrder ==> Take OrderRequest , string Email ==> Return OrderReuslt.
        Task<OrderResult> CreateOrderAsync(OrderRequest order, string userEmail);
        // GetDelivery Method ==> Take nothing ==> return IEnummerable <DeliveryMethodResult>
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
