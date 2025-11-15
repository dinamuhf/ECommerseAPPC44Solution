using ECommerseAPPC04.CustomsMiddleWares;
using Microsoft.EntityFrameworkCore.Internal;

namespace ECommerseAPPC04.Extentions
{
    public static class WebApplicationExtentions
    {
       public static async Task<WebApplication> SeedDbAsync( this WebApplication app)
        {
            using var Scope= app.Services.CreateScope();
            var dbInitializer=Scope.ServiceProvider.GetRequiredService<IDbSetInitializer>();
            await dbInitializer InitializeAsync();
            return app;
        }
        public static WebApplication UseCustomMiddlewareExeptions(this WebApplication app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
    }
}
