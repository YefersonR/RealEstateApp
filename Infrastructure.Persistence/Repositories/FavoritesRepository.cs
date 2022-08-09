using Core.Domain.Entities;
using Infrastructure.Persistence.Context;
using SocialNetwork.Core.Application.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class FavoritesRepository : GenericRepository<Favorites>, IFavoritesRepository
    {
        private readonly RealEstateContext _dbContext;

        public FavoritesRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
