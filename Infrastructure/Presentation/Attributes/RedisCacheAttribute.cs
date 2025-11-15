using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.Contracts;
using System.Text;

namespace Presentation.Attributes
{
    public class RedisCacheAttribute(int durationInSeconds = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            string key = GenerateKey(context.HttpContext.Request);
            var result = await cacheService.GetCachedValueAsync(key);

            if (result != null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var resultContext = await next.Invoke();

            if (resultContext.Result is OkObjectResult okObjResult)
            {
                await cacheService.SetCacheValueAsync(key, okObjResult, TimeSpan.FromSeconds(durationInSeconds));
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            var key = new StringBuilder();

            key.Append(request.Path); // /api/products

            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                ///api/Products?pageSize=10-sort=NameDesc-pageIndex=1
                key.Append($"{item.Key}-{item.Value}");
            }

            return key.ToString();
        }
    }
}
