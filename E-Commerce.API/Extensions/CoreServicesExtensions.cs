using Services;
using Services.Abstraction.Contracts;
using Services.Implementation;
using Shared.Common;

namespace E_Commerce.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(ProjectReference).Assembly);      //just empty class to recognize the assembly which the mapping profiles found.
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
