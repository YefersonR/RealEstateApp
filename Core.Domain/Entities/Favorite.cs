using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Favorite : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public int EstateId { get; set; }
        
        public Estate Estates { get; set; }
    }
}
