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
    public class GetEstateByCodeQuery : IRequest<EstateRequest>
    {
        public string Code { get;  set; }
    }
    public class GetEstateByIdQueryHandler : IRequestHandler<GetEstateByCodeQuery, EstateRequest>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public GetEstateByIdQueryHandler(IEstatesRepository estatesRepository, IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _estatesRepository = estatesRepository;
            _mapper = mapper;
        }
        public async Task<EstateRequest> Handle(GetEstateByCodeQuery request, CancellationToken cancellationToken)
        {
            var estate = await GetWithIncludeByCode(request.Code);
            return estate;
        }
        public async Task<EstateRequest> GetWithIncludeByCode(string Code)
        {
            var estateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs", "FeaturesRelations" });
            var estate = estateList.Where(x => x.Code == Code).FirstOrDefault();
            var estateRequest = _mapper.Map<EstateRequest>(estate);
            estateRequest.FeaturesRelations.ForEach(x => x.Features = _mapper.Map<FeaturesRequest>(_featuresRepository.GetByIdAsync(x.FeatureId).Result));
            return estateRequest;
        }
    }
}
