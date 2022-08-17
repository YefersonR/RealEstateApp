using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Estates
{
    public class EstateRequest
    {
        public string Code { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }
        public int Toilets { get; set; }
        public string Description { get; set; }
        public string ImageProfile { get; set; }
        public int AgentId { get; set; }
        
        //public int SellTypeId { get; set; }
        //public int EstateTypesId { get; set; }
        public SellTypeRequest SellTypes { get; set; }
        public EstateTypeRequest EstateTypes { get; set; }
    }
}
