using Microsoft.Extensions.DependencyInjection;

namespace TacticalShop.Backend.Extensions.ServiceCollection
{
    public static class AuthenticationRegister
    {
        public static void AddAuthenAuthor(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddLocalApi("Bearer", option =>
                {
                    option.ExpectedScope = "tacticalshop.api";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
