using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager):ApiController
    {
        //CreateOrder
        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest orderRequest)
        {
            // i can't take the Email of the user from the body i must take it from the token 
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManager.OrderService.CreateOrderAsync(orderRequest, userEmail);
            return Ok(order);
        }
        //GetOrderById
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id)
            => Ok(await _serviceManager.OrderService.GetOrderByIdAsync(id));
        //GetAllOrdersByEmail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrdersByEmailAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManager.OrderService.GetOrderByEmailAsync(userEmail);
            return Ok(orders);
        }
        //GetDeliveryMethods
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
            =>  Ok(await _serviceManager.OrderService.GetDeliveryMethodsAsync());
    }
}
