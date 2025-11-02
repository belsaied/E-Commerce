using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace E_Commerce.API.Extensions
{
    public static class WebApiServicesExtensions
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            // Add services to the container.
            // using AddJsonOptions to handle the Enum vlaues in the drop down of the Swagger.
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            return services;
        }
    }
}
