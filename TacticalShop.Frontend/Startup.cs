using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TacticalShop.Frontend.Services;


namespace TacticalShop.Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            //services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddAuthentication(options =>
           {
               options.DefaultScheme = "Cookies";
               options.DefaultChallengeScheme = "oidc";
           })
               .AddCookie("Cookies")
               .AddOpenIdConnect("oidc", options =>
               {


                   options.Authority = Configuration.GetServiceUri("backend").ToString();

                   options.RequireHttpsMetadata = false;
                   options.GetClaimsFromUserInfoEndpoint = true;

                   options.ClientId = "mvc";
                   options.ClientSecret = "secret";
                   options.ResponseType = "code";

                   options.SaveTokens = true;

                   options.Scope.Add("openid");
                   options.Scope.Add("profile");
                   options.Scope.Add("tacticalshop.api");

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = "name",
                       RoleClaimType = "role"
                   };
               });

            var configureClient = new Action<IServiceProvider, HttpClient>(async (provider, client) =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");

                client.BaseAddress = Configuration.GetServiceUri("backend");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<IProductApiClient, ProductApiClient>(configureClient);
            services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(configureClient);
            services.AddHttpClient<IBrandApiClient, BrandApiClient>(configureClient);
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
