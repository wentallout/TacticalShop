using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TacticalShop.Backend.IdentityServer;
using TacticalShop.Backend.Models;

namespace TacticalShop.Backend.Extensions.ServiceCollection
{
    public static class IdentityServerRegister
    {
        public static void AddIdentityServerCustom(this IServiceCollection services, IConfiguration configuration)
        {
            var clientUrls = new Dictionary<string, string>
            {
                ["Mvc"] = configuration["ClientUrl:Mvc"],
                ["Swagger"] = configuration["ClientUrl:Swagger"]
                //["React"] = Configuration["ClientUrl:React"]
            };

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
                .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
                .AddInMemoryClients(IdentityServerConfig.Clients(clientUrls))
                .AddAspNetIdentity<User>()
                .AddProfileService<CustomProfileService>()
                .AddDeveloperSigningCredential();
        }
    }
}