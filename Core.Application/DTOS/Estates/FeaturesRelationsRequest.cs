using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Estates
{
    public class FeaturesRelationsRequest
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int EstateId { get; set; }

        public FeaturesRequest Features { get; set; } 
        public EstateRequest Estates { get; set; }
    }
}
