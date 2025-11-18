using Microsoft.AspNetCore.Authentication.JwtBearer;
using StackExchange.Redis;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace E_CommerceWebAPPC44G01.Extensions
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddSwagerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Enter 'Bearer' Followed By  Space And Your  Token"

                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                         {
                             Reference= new OpenApiReference
                             {
                                 Id="Bearer",
                                 Type=ReferenceType.SecurityScheme

                             }
                         },
                          new string []{}

                    }

                });
            });


            return Services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });
            return Services;
        }

        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],

                    ValidateAudience = true,

                    ValidAudience = configuration["JWTOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"]))


                };
            });
            return services;

        }


    }
}

