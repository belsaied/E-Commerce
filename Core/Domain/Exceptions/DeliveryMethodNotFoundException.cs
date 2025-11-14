namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException:NotFoundException
    {
        public DeliveryMethodNotFoundException(int id):base($"The Delivery Method with Id {id} Not found")
        {
            
        }
        public DeliveryMethodNotFoundException(string msg):base(msg)
        {
            
        }
    }
}
