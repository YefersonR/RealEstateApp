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
    public class SellTypesRepository : GenericRepository<SellTypes>, ISellTypesRepository
    {
        private readonly RealEstateContext _dbContext;

        public SellTypesRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
