using Services;
using Services.Abstraction.Contracts;
using Services.Implementation;

namespace E_Commerce.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(ProjectReference).Assembly);      //just empty class to recognize the assembly which the mapping profiles found.
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
