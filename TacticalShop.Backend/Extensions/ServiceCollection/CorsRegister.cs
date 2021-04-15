using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TacticalShop.Backend.Configs;

namespace TacticalShop.Backend.Extensions.ServiceCollection
{
    public static class CorsRegister
    {
        public static void AddCorsOrigins(this IServiceCollection services, IConfiguration configuration)
        {
            var clientUrls = new Dictionary<string, string>
            {
                ["Mvc"] = configuration["ClientUrl:Mvc"],
                ["Swagger"] = configuration["ClientUrl:Swagger"],
                //["React"] = Configuration["ClientUrl:React"]
            };
            services.AddCors(options =>
            {
                options.AddPolicy(AllowOrigins.MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(clientUrls["Mvc"], clientUrls["Swagger"])
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}