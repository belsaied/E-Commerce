using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositories;
using Services;
using Services.Abstraction.Contracts;
using Services.Implementation;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using E_Commerce.API.Middlewares;
using E_Commerce.API.Factories;
namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // using AddJsonOptions to handle the Enum vlaues in the drop down of the Swagger.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            // allow DI for DbContext.
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(cfg => { } , typeof(ProjectReference).Assembly);      //just empty class to recognize the assembly which the mapping profiles found.
            builder.Services.AddScoped<IServiceManager,ServiceManager>();  
            var app = builder.Build();
            #region Call SeedData before Any Request.
            // to get an instance of DataSeeding Manually and call the method SeedData before the request executed.
           using var scope = app.Services.CreateScope();
            var objOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
           await objOfDataSeeding.SeedDataAsync();
            #endregion
            // Configure the HTTP request pipeline.
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
