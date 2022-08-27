using AutoMapper;
using Core.Application.DTOS.Estates;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Estates.Queries.GetEstateByCode
{
    /// <summary>
    /// Parametro para la obtener de una propiedad por el codigo
    /// </summary>
    public class GetAllEstatesByAgentIdQuery : IRequest<List<EstateRequest>>
    {
        public string AgentId { get;  set; }
    }
    public class GetAllEstatesByAgentIdQueryHandler : IRequestHandler<GetAllEstatesByAgentIdQuery, List<EstateRequest>>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public GetAllEstatesByAgentIdQueryHandler(IEstatesRepository estatesRepository, IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _estatesRepository = estatesRepository;
            _mapper = mapper;
        }
        public async Task<List<EstateRequest>> Handle(GetAllEstatesByAgentIdQuery request, CancellationToken cancellationToken)
        {
            var estate = await GetWithIncludeByCode(request.AgentId);
            return estate;
        }
        public async Task<List<EstateRequest>> GetWithIncludeByCode(string AgentId)
        {
            var estateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs", "FeaturesRelations" });
            var estate = estateList.Where(x => x.AgentId == AgentId).ToList();
            var estateRequest = _mapper.Map<List<EstateRequest>>(estate);
            foreach(var estateItem in estateRequest)
            {
                estateItem.FeaturesRelations.ForEach(x => x.Features = _mapper.Map<FeaturesRequest>(_featuresRepository.GetByIdAsync(x.FeatureId).Result));
            }
            return estateRequest;
        }
    }
}
