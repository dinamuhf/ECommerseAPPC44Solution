
using DomainLayer.Contracts;
using DomianLayer.Contracts;
using E_CommerceWebAPPC44G01.CustomMiddleWares;
using E_CommerceWebAPPC44G01.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Data.DataSeed;
using Persistance.Repositories;
using Presistance.Repositories;
using Service;
using ServiceAbstraction;
using Services.MappingProfiles;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace E_CommerceWebAPPC44G01
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwagerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices(builder.Configuration);
            builder.Services.AddJwtService(builder.Configuration);





            var app = builder.Build();
            await app.SeedDataBaseAsync();
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration = true

                    };
                    options.DocumentTitle = "My E-Commerce API";
                    options.JsonSerializerOptions = new System.Text.Json.JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    options.DocExpansion(DocExpansion.None);
                    options.EnableFilter();
                    options.EnablePersistAuthorization();
                });

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseCors("AllowAll");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
