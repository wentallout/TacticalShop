using Microsoft.AspNetCore.Hosting;
using TacticalShop.Backend.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace TacticalShop.Backend.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}