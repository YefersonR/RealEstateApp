using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Estate : AuditableBaseEntity
    {
        public string Code { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }
        public int Toilets { get; set; }
        public string Description { get; set; }
        public int EstateTypesId { get; set; }
        public int AgentId { get; set; }        
        public List<Feature> FeaturesIds { get; set; }
        public int SellTypeId { get; set; }

        public SellType SellTypes { get; set; }
        public EstateType EstateTypes { get; set; }
        public ICollection<EstatesImg> EstatesImgs { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<FeaturesRelations> FeaturesRelations { get; set; }

    }
}
