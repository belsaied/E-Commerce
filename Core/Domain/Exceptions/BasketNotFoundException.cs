namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException:NotFoundException
    {
        public BasketNotFoundException(string id) : base($"Basket with Id {id} Not found")
        {
            
        }
    }
}
