using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Service
{
    public static  class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(MappingProfiles).Assembly);



            Services.AddScoped<IServiceManager, ServiceManager>();

            return Services;
        }
    }
}
