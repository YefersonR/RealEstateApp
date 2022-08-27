using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Estates
{
    public class FeaturesRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<FeaturesRelationsRequest> FeaturesRelations { get; set; }
    }
}
