using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    public class OrderWithIncludesSpecifications :BaseSpecifications<Order,Guid>
    {
        // GetOrderById ==> [Criteria => o.id == id] ==> [Includes] ==> DeliveryMethods & OrderItems (Navigations i must load before i get data to get the related data in the response)
        public OrderWithIncludesSpecifications(Guid id):base(o => o.Id == id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
        // GetAllOrdersByEmail ==> [Criteria => o.Email == userEmail] ==> [Includes] ==> DeliveryMethods & OrderItems (Navigations i must load before i get data to get the related data in the response)
        public OrderWithIncludesSpecifications(string userEmail):base(o=> o.UserEmail == userEmail)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
            AddOrderBy(o => o.OrderDate);
            // to order the Orders depending on the Date not the Id => [Guid] which generates differently in each request.
        }
    }
}
