namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(int id)
            :base($"Product with Id {id} Not found")
        {
            
        }
    }
}
