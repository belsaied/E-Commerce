using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController(IServiceManager _serviceManager):ApiController
    {
        //Get
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok (await _serviceManager.basketService.GetBasketAsync(id));
        //CreateOrUpdate
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateAsync(BasketDto basket)
            => Ok (await _serviceManager.basketService.CreateOrUpdateBasketAsync(basket));
        //Delete (send the Id in the Request as a segment not fromQuery).
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            await _serviceManager.basketService.DeleteBasketAsync(id);
            return NoContent();   //204
        }
    }
}
