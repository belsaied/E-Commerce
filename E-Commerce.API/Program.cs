using E_Commerce.API.Extensions;
namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            #region DI Container.
            var builder = WebApplication.CreateBuilder(args);
            // Web API services.
            builder.Services.AddWebApiServices();

            // Infrastructure Services.
            builder.Services.AddInfrasturctureServices(builder.Configuration);

            // Core Services.
            builder.Services.AddCoreServices(builder.Configuration);
            #endregion

            #region Piplines.
            var app = builder.Build();
            // Data Seeding Service (put before the first request)
            await app.SeedDatabaseAsync();
            // UseExceptionHandlingMiddleWare:
            app.UseExceptionHandlingMiddleWare();
            // Use swagger MiddleWares so i cant customize without the program be large.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}

