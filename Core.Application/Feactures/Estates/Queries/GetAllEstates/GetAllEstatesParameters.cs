using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Estates.Queries.GetAllEstates
{
    public class GetAllEstatesParameters
    {
        public int? SellTypeId { get; set; }
        public double? Price { get; set; }
        public int? Rooms { get; set; }
        public int? Toilets { get; set; }

    }
}
