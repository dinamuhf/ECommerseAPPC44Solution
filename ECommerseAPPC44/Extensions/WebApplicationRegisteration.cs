using DomainLayer.Contracts;

namespace E_CommerceWebAPPC44G01.Extensions
{
    public  static class WebApplicationRegisteration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            var Scope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();
        }
    }
}
