using Shared.Dtos.BasketModule;

namespace Services.Abstraction.Contracts
{
    public interface IBasketService
    {
        // Get
        Task<BasketDto> GetBasketAsync(string id);
        // Delete
        Task<bool> DeleteBasketAsync(string id);
        // CreateOrUpdate
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
    }
}
