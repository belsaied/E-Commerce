using Shared.Dtos.BasketModule;

namespace Services.Abstraction.Contracts
{
    public interface IPaymentService
    {
        // Signature for a method to create or update payment that return basketDto
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
    }
}
