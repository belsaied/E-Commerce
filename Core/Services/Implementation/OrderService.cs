using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModule;
// using ShippingAddress = Domain.Entities.OrderModule.Address;
namespace Services.Implementation
{
    public class OrderService(IMapper mapper , IBasketRepository basketRepository , IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            // Map Address AKA[ShippingAddress]
            var address = mapper.Map<Address>(orderRequest.ShippingAddress);
            // Get Basket using BasketId.
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId) ?? throw new BasketNotFoundException(orderRequest.BasketId);
            // Get OrderItems before adding them to DB.
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.BasketItems)
            {
                var product = await unitOfWork.GetReopsitory<Product, int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
            // Delivery Method.
            var deliveryMethod = await unitOfWork.GetReopsitory<DeliveryMethod, int>().GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
            // SubTotal
            var subTotal = orderItems.Sum(item => item.Price *  item.Quantity);
            // *******Create order
            var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal);
            // Save to DB
            await unitOfWork.GetReopsitory<Order, Guid>().AddAsync(orderToCreate);
            await unitOfWork.SaveChangesAsync();
            // Map & Return .
            return mapper.Map<OrderResult>(orderToCreate);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
            => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), product.Price, item.Quantity);

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.GetReopsitory<DeliveryMethod, int>()
                                                   .GetAllAsync();

            return mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }


        public async Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string userEmail)
        {
            var orders = await unitOfWork.GetReopsitory<Order,Guid>()
                .GetAllAsync(new OrderWithIncludesSpecifications(userEmail));
            return mapper.Map<IEnumerable<OrderResult>>(orders);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetReopsitory<Order, Guid>()
                .GetByIdAsync(new OrderWithIncludesSpecifications(id)) ?? throw new OrderNotFoundException(id);
            return mapper.Map<OrderResult>(order);
        }
    }
}
