using Core.Application.Interface.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class EstateTypesRepository : GenericRepository<EstateType>, IEstateTypesRepository
    {
        private readonly RealEstateContext _dbContext;

        public EstateTypesRepository(RealEstateContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task DeleteAsync(EstateType entity)
        {
            var listEstates = await _dbContext.Set<Estate>().ToListAsync();
            listEstates = listEstates.Where(estate=>estate.EstateTypesId == entity.Id).ToList();
            foreach(Estate estate in listEstates)
            {
                _dbContext.Set<Estate>().Remove(estate);
                await _dbContext.SaveChangesAsync();
            }
            await base.DeleteAsync(entity);

        }
    }
}
