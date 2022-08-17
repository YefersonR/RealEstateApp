using Core.Application.Interface.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class FeaturesRelationsRepository : GenericRepository<FeaturesRelations>, IFeaturesRelationsRepository
    {
        private readonly RealEstateContext _dbContext;

        public FeaturesRelationsRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
