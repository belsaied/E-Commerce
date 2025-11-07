using Domain.Contracts;
using E_Commerce.API.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace E_Commerce.API.Extensions
{
    public  static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            #region Call SeedData before Any Request.
            // to get an instance of DataSeeding Manually and call the method SeedData before the request executed.
            using var scope = app.Services.CreateScope();
            var objOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objOfDataSeeding.SeedDataAsync();
            await objOfDataSeeding.SeedIdentityDataAsync();
            #endregion
            return app;
        }
        public static WebApplication UseExceptionHandlingMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }
        
        public static WebApplication UseSwaggerMiddlewares(this  WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ConfigObject = new Swashbuckle.AspNetCore.SwaggerUI.ConfigObject()
                {
                    DisplayRequestDuration = true
                };
                options.DocumentTitle = "My E-Commerce API";
                options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                options.DocExpansion(DocExpansion.None);
                options.EnableFilter();
                options.EnablePersistAuthorization();
            });
            return app;
        }
    }
}
