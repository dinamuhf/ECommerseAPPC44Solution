using DomainLayer.Contracts;
using ECommerseAPPC04.CustomsMiddleWares;
using ECommerseAPPC04.Extentions;
using ECommerseAPPC04.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presistance.Data;
using Presistance.Data.DataSeed;
using Presistance.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErrorModels;
using StackExchange.Redis;

namespace ECommerseAPPC04
{
    public class Program
    {
        public  static  async Task<ConnectionMultiplexer> Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region configure services
          builder.Services.AddInfraStructureService(builder.Configuration);
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddCoreServices();
            builder.Services.AddPresentationServices();
            builder.Services.AddScoped<IBasketRepository,BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_))=>{
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RediscConnection"));
                    }
            }

            #endregion


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            var app = builder.Build();
            #region Services
            var Scope = app.Services.CreateScope();
            var ObjectOfdataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
          await  ObjectOfdataSeeding.DataSeedAsync();
            #endregion
          app.UseCustomMiddlewareExeptions();
          await  app.SeedDbAsync();
            // Configure the HTTP request pipeline.
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
