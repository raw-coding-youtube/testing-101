using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RawCoding.WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Privileged", policy => policy
                    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
                    .RequireClaim("CustomRoleType", "God", "Angel")
                    .RequireAuthenticatedUser());

                options.AddPolicy("Administrator", policy => policy
                    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
                    .RequireClaim("CustomRoleType", "God")
                    .RequireAuthenticatedUser());
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}