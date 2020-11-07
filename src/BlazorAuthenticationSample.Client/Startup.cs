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
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultTokenProviders();

            //services. AddIdentity<ApplicationUser , ApplicationRole>()
            //   .AddDefaultTokenProviders();
            services.AddTransient<IUserStore<ApplicationUser>, CustomUserStore>();
            //services.AddTransient<IRoleStore<ApplicationRole>, CustomRoleStore>();

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
            //services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp =>
            {
                // this is safe because 
                //     the `RevalidatingIdentityAuthenticationStateProvider` extends the `ServerAuthenticationStateProvider`
                var provider = (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>();
                return provider;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            //, DbContextOptions<ApplicationDbContext> identityDbContextOptions, 
            //UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager
            )
        {
            //EnsureTestUsers(identityDbContextOptions, userManager, roleManager);

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

        //private static void EnsureTestUsers(DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        //{
        //    using (var db = new ApplicationDbContext(identityDbContextOptions))
        //    {
        //        db.Database.EnsureCreated();
        //    }

        //    var user = userManager.FindByEmailAsync("user@example.com").GetAwaiter().GetResult();
        //    if (user == null)
        //    {
        //        userManager.CreateAsync(new IdentityUser("user@example.com") { Email = "user@example.com", EmailConfirmed = true }, "1234").GetAwaiter().GetResult();
        //    }

        //    var userWithUnconfirmedEmailAddress = userManager.FindByEmailAsync("anotheruser@example.com").GetAwaiter().GetResult();
        //    if (userWithUnconfirmedEmailAddress == null)
        //    {
        //        userManager.CreateAsync(new IdentityUser("anotheruser@example.com") { Email = "anotheruser@example.com", EmailConfirmed = false }, "1234").GetAwaiter().GetResult();
        //    }

        //    var admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
        //    if (admin == null)
        //    {
        //        userManager.CreateAsync(new IdentityUser("admin@example.com") { Email = "admin@example.com", EmailConfirmed = true }, "1234").GetAwaiter().GetResult();
        //        admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
        //    }

        //    if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
        //    {
        //        roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
        //    }

        //    if (!userManager.IsInRoleAsync(admin, "Admin").GetAwaiter().GetResult())
        //    {
        //        userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
        //    }
        //}
    }
}
