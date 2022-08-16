﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Estates
{
    public class EstateRequest
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }
        public int Toilets { get; set; }
        public string Description { get; set; }
        public int AgentId { get; set; }
        public SellTypeRequest SellTypes { get; set; }
        public List<EstateImgRequest> EstatesImgs { get; set; }
        public List<FeaturesRelationsRequest> FeaturesRelations { get; set; }
        public EstateTypeRequest EstateTypes { get; set; }
    }
}