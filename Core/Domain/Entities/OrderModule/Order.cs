using ShippingAddress = Domain.Entities.OrderModule.Address;     // just for Alias Name.
using Domain.Entities.ProductModule;

namespace Domain.Entities.OrderModule
{
    public class Order :BaseEntity<Guid>
    {
        public Order(string userEmail, ShippingAddress shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal,string paymentIntentId)
        {
            // SQL does't initialize the Guid values it is the EF who do it so i must put it so he can initialize a new Guid as an Id.
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }
        public Order()
        {
            
        }
        public string UserEmail { get; set; } = string.Empty;
        public ShippingAddress ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; set; } = string.Empty;

    }
}
