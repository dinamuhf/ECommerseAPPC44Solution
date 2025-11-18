using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data.DataSeed;
using Persistance.Data;
using Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance.Data.Identity;
using DomianLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Presistance.Data;
using Presistance.Data.DataSeed;
using Presistance.Repositories;

namespace Persistance
{
    public static  class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services,IConfiguration Configuration)
        {
        Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
           Services.AddScoped<IDataSeeding, DataSeeding>();
          Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            Services.AddIdentityCore<ApplicationUser>()
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return Services;
        }
    }
}
