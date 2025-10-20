
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // allow DI for DbContext.
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            var app = builder.Build();
            #region Call SeedData before Any Request.
            // to get an instance of DataSeeding Manually and call the method SeedData before the request executed.
           using var scope = app.Services.CreateScope();
            var objOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            objOfDataSeeding.SeedData(); 
            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
