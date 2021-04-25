using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TacticalShop.Backend.Areas.Identity.IdentityHostingStartup))]
namespace TacticalShop.Backend.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}