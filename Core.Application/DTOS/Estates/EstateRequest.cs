using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Estates
{
    public class EstateRequest
    {
        public int Id { get; set; } //string
        public string Code { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }
        public int Toilets { get; set; }
        public string Description { get; set; }
        public string AgentId { get; set; }
        public int FavoriteId { get; set; }
        public SellTypeRequest SellTypes { get; set; }
        public List<EstateImgRequest> EstatesImgs { get; set; }
        public List<FeaturesRelationsRequest> FeaturesRelations { get; set; }
        public EstateTypeRequest EstateTypes { get; set; }
        public AgentesViewModel Agente { get; set; }
    }
}
