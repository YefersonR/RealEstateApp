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
    public class EstatesImgRepository : GenericRepository<EstatesImg>, IEstatesImgRepository
    {
        private readonly RealEstateContext _dbContext;

        public EstatesImgRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
