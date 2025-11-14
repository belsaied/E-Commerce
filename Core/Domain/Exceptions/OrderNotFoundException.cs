namespace Domain.Exceptions
{
    public class OrderNotFoundException:NotFoundException
    {
        public OrderNotFoundException(Guid id) : base($"Order with Id {id} Not found")
        {
            
        }
    }
}
