
namespace Domain.Entities.ProductModule
{
    public class BaseEntity<TKye>  // Generic Class to specify whether the Id : int,Guid,String.
    {
        public TKye Id { get; set; }
    }
}
