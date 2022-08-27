using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Feature : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<FeaturesRelations> FeaturesRelations { get; set; }
    }
}
