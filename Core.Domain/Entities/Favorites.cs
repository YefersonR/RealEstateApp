using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Favorites : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public int EstateId { get; set; }
        
        public Estates Estates { get; set; }
    }
}
