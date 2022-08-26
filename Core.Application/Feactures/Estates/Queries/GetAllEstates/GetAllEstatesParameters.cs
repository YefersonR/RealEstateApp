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
        public double? MaxPrice { get; set; }
        public double? MinPrice { get; set; }
        public int? Rooms { get; set; }
        public bool? FavOnly { get; set; }
        public int? Toilets { get; set; }
        public string? FavUserId { get; set; }
        public String? AgentID { get; set; }

    }
}
