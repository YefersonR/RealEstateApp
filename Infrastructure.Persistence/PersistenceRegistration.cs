using Core.Application.Interface.Repositories;
using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class PersistenceRegistration
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<RealEstateContext>(options =>
                        options.UseInMemoryDatabase("InMemoryDatabase"));
            }
            else
            {
                services.AddDbContext<RealEstateContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("RealStateConnection"), m =>
                            m.MigrationsAssembly(typeof(RealEstateContext).Assembly.FullName)));
            }

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IEstatesImgRepository, EstatesImgRepository>();
            services.AddTransient<IEstatesRepository, EstatesRepository>();
            services.AddTransient<IEstateTypesRepository, EstateTypesRepository>();
            services.AddTransient<IFavoritesRepository, FavoritesRepository>();
            services.AddTransient<IFeaturesRepository, FeaturesRepository>();
            services.AddTransient<ISellTypesRepository, SellTypesRepository>();
            services.AddTransient<IFeaturesRelationsRepository, FeaturesRelationsRepository>();
        }
    }
}
