namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id):NotFoundException($"The Delivery Method with Id {id} Not found")
    {

    }
}
