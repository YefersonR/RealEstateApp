using Infrastructure.Identity.Context;
using Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Core.Application.Inferfaces.Service;
using Infrastructure.Identity.Services;

namespace Infrastructure.Identity
{
    public static class IdentityRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context

            if (configuration.GetValue<bool>("InMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion

            #region Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account";
                option.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddAuthentication();

            #endregion
            #region Services

            services.AddTransient<IAccountService,AccountService>();
            #endregion

        }
    }
}
