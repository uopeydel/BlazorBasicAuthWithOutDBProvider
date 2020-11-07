using BlazorAuthenticationSample.Client.CustomProvider;
//using BlazorAuthenticationSample.Client.Data;
using BlazorAuthenticationSample.Client.Features.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorAuthenticationSample.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
             
            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultTokenProviders(); 

            services.AddTransient<IUserStore<ApplicationUser>, CustomUserStore>(); 

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", c => c.RequireRole("Admin"));

                options.AddPolicy("KERole", policy =>
                    policy.Requirements.Add(new CompanyRoleRequirement("ITBackOffice")));
            });
            services.AddSingleton<IKEAppProfileProvider, KEAppProfileProvider>();
            services.AddSingleton<IAuthorizationHandler, CompanyRoleHandler>();

            services.AddRazorPages();
            services.AddControllers();
            services.AddServerSideBlazor();
            
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp =>
            { 
                var provider = (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>();
                return provider;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env 
            )
        { 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
         
    }
}
