using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interface.Repositories
{
    public interface IEstatesRepository : IGenericRepository<Estate>
    {
        Task<Estate> GetByCodeAsync(string Code);
        Task Update(Estate entity, string Code);
    }
}
