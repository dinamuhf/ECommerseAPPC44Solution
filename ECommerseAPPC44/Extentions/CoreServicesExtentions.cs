using Service;
using ServiceAbstraction;

namespace ECommerseAPPC04.Extentions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(services.AssemblyRefernce).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }

    }
}
