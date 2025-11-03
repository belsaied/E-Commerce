using Domain.Entities.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        // Get Basket By Id
        Task<CustomerBasket?> GetBasketAsync(string id);
        // Create or Update Basket
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null);
        // Delete Basket
        Task<bool> DeleteBasketAsync(string id);
    }
}
