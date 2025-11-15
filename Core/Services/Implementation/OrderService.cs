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
            var address = mapper.Map<Address>(orderRequest.ShipToAddress);
            // Get Basket using BasketId.
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId) ?? throw new BasketNotFoundException(orderRequest.BasketId);
            // Get OrderItems before adding them to DB.
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product = await unitOfWork.GetReopsitory<Product, int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
            var orderRepo = unitOfWork.GetReopsitory<Order, Guid>();

            //[3] GetDeliveryMethod ==> DeliverMethodId = DB
            var deliveryMethod = await unitOfWork.GetReopsitory<DeliveryMethod, int>()
                .GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            var orderExisit = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId));
            if (orderExisit != null)
            {
                orderRepo.Delete(orderExisit);
                //ORDERITEMS ==> Order
            }

            //[4] Calculate SubTotal ==> OrderItem.Q * OrderItem.Price
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            //[5] Create obj from order ==> params , Add DB , Save Changes
            var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal, basket.PaymentIntentId);
            await orderRepo.AddAsync(orderToCreate);
            await unitOfWork.SaveChangesAsync();

            //[6] Map <order, OrderResult>
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
