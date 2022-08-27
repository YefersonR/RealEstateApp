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
    public class EstatesRepository : GenericRepository<Estate>, IEstatesRepository
    {
        private readonly RealEstateContext _dbContext;

        public EstatesRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Estate> GetByCodeAsync(string Code)
        {
            return await _dbContext.Set<Estate>().FindAsync(Code);
        }

        public async Task Update(Estate entity, int Id)
        {
            Estate entry = await _dbContext.Set<Estate>().FindAsync(Id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
