using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using Stripe;
using Product = Domain.Entities.ProductModule.Product;
namespace Services.Implementation
{
    public class PaymentService(IConfiguration _configuration, IBasketRepository _basketRepository
        ,IUnitOfWork _unitOfWork ,IMapper _mapper) : IPaymentService
    {
        //public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        //{
        //    //[0] Install stripe.net
        //    //[1] Set up key [secret key] ==> stripe key
        //    StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

        //    //[2] Get basket [by basketId]
        //    var basket = await _basketRepository.GetBasketAsync(basketId)
        //        ?? throw new BasketNotFoundException(basketId);

        //    //[3] Validate items price ==> [basket.item.price = product.price] ==> product from db
        //    foreach (var item in basket.BasketItems)
        //    {
        //        var product = await _unitOfWork.GetReopsitory<Product, int>().GetByIdAsync(item.Id)
        //            ?? throw new ProductNotFoundException(item.Id);

        //        item.Price = product.Price;
        //    }

        //    //[4] Validate shipping price ==> get deliveryMethod [DeliveryMethodId] ==> shippingPrice = DeliveryMethod.Price
        //    if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundException("No delivery method selected");

        //    var deliveryMethod = await _unitOfWork.GetReopsitory<DeliveryMethod, int>()
        //        .GetByIdAsync(basket.DeliveryMethodId.Value)
        //        ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

        //    basket.ShippingPrice = deliveryMethod.Price;
        //    //[5] Total ==> [SubTotal + ShippingPrice] ==> cent ==> * 100 ==> Long
        //    //      ==> (long) ((basket.items.q * basket.items.price) + shippingPrice [DeliveryMethod.Price]) * 100
        //    var amount = (long)(basket.BasketItems.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;

        //    //[6] Create or update paymentIntentId
        //    var stripeService = new PaymentIntentService();

        //    if (string.IsNullOrEmpty(basket.PaymentIntentId))
        //    {
        //        // create
        //        var options = new PaymentIntentCreateOptions()
        //        {
        //            Amount = amount,      // total = subtotal + shippingPrice
        //            Currency = "USD",     // dollar
        //            PaymentMethodTypes = ["card"]
        //        };

        //        var paymentIntent = await stripeService.CreateAsync(options);
        //        basket.PaymentIntentId = paymentIntent.Id;
        //        basket.ClientSecret = paymentIntent.ClientSecret;
        //    }
        //    else
        //    {
        //        var options = new PaymentIntentUpdateOptions()
        //        {
        //            Amount = amount
        //        };

        //        await stripeService.UpdateAsync(basket.PaymentIntentId, options);
        //    }

        //    //[7] Save changes [Update] Basket
        //    await _basketRepository.CreateOrUpdateBasketAsync(basket);

        //    //[8] Map to basketDto ==> return
        //    return _mapper.Map<BasketDto>(basket);


        //}
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            //[0] Install stripe.net
            //[1] Set up key [secret key] ==> stripe key
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];
            //[2] Get basket [by basketId]
            var basket = await GetBasketAsync(basketId);
            //[3] Validate items price ==> [basket.item.price = product.price] ==> product from db
            await ValidateBasketAsync(basket);
            // 4] Calculate Total amount
            var amount = CalculateTotalAsync(basket);
            // 5] Create or update paymentIntentId
            await CreationOrUpdatePaymentIntentAsync(basket, amount);
            // 6] Save changes [Update] Basket
            await _basketRepository.CreateOrUpdateBasketAsync(basket);
            // 7] Map to BasketDto.
            return _mapper.Map<BasketDto>(basket);

        }

        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long amount)
        {
            var stripeService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,      // total = subtotal + shippingPrice
                    Currency = "USD",     // dollar
                    PaymentMethodTypes = ["card"]
                };

                var paymentIntent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount
                };

                await stripeService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }

        private long CalculateTotalAsync(CustomerBasket basket)
        {
            var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;
            return amount;
        }



        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetReopsitory<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }
            // Validate shipping Price too.
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundException("No delivery method selected");

            var deliveryMethod = await _unitOfWork.GetReopsitory<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Price;
        }

        private async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return await _basketRepository.GetBasketAsync(basketId)
               ?? throw new BasketNotFoundException(basketId);
        }
    }
}
