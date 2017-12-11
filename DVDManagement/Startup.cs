using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DVDManagement.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Linq;

namespace DVDManagement
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
            services.AddDbContext<DVDMAGContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication("MyCookieAuthenticationScheme")
                    .AddCookie("MyCookieAuthenticationScheme", options =>
                    {
                        options.Events = new CookieAuthenticationEvents
                        {
                            //OnValidatePrincipal = LastChangedValidator.ValidateAsync
                        };
                    });

            services.AddMvc();
        }

        /*
        public static class LastChangedValidator
        {
            public static async Task ValidateAsync(CookieValidatePrincipalContext context)
            {
                // Pull database from registered DI services.
                var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                var userPrincipal = context.Principal;

                // Look for the last changed claim.
                string lastChanged;
                lastChanged = (from c in userPrincipal.Claims
                               where c.Type == "LastUpdated"
                               select c.Value).FirstOrDefault();

                if (string.IsNullOrEmpty(lastChanged) ||
                    !userRepository.ValidateLastChanged(userPrincipal, lastChanged))
                {
                    context.RejectPrincipal();
                    await context.HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
                }
            }
        }
        */

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseAuthentication();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
