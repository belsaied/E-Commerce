using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Shared.Dtos.OrderModule;
using ShippingAddress = Domain.Entities.OrderModule.Address;
namespace Services.Implementation
{
    public class OrderService(IMapper mapper , IBasketRepository basketRepository , IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest order, string userEmail)
        {
            // Map Address AKA[ShippingAddress]
            var ShippingAddress = mapper.Map<ShippingAddress>(order.ShippingAddress);
            // Get Basket using BasketId.
            var basket = await basketRepository.GetBasketAsync(order.BasketId) ?? throw new BasketNotFoundException(order.BasketId);
            // Get OrderItems before adding them to DB.
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.BasketItems)
            {
                var product = await unitOfWork.GetReopsitory<Product, int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
            // Delivery Method.
            var deliveryMethod = await unitOfWork.GetReopsitory<DeliveryMethod, int>().GetByIdAsync(order.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);
            // SubTotal
            var subTotal = orderItems.Sum(item => item.Price *  item.Quantity);
            // *******Create order
            var Order = new Order(userEmail, ShippingAddress, orderItems, deliveryMethod, subTotal);
            // Save to DB
            await unitOfWork.GetReopsitory<Order, Guid>().AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
            // Map & Return .
            return mapper.Map<OrderResult>(Order);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
            => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);

        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
