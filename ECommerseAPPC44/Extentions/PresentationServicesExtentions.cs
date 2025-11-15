using ECommerseAPPC04.Factories;
using Microsoft.AspNetCore.Mvc;
using Service;
using ServiceAbstraction;

namespace ECommerseAPPC04.Extentions
{
    public static class PresentationServicesExtentions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyRefrence).Assembly);
            services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateApiValidationErrorResponse;

            });
return services;
        }
    }
}
