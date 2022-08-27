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

namespace Core.Application.Feactures.Feactures.Queries.GetAllFeactures
{
    public class GetAllFeacturesQuery : IRequest<List<FeaturesRequest>>
    {
    }
    public class GetAllFeacturesQueryHandler : IRequestHandler<GetAllFeacturesQuery, List<FeaturesRequest>>
    {
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public GetAllFeacturesQueryHandler(IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _mapper = mapper;
        }
        public async Task<List<FeaturesRequest>> Handle(GetAllFeacturesQuery request, CancellationToken cancellationToken)
        {
            var data = await _featuresRepository.GetAllWhitIncludes(new List<string> { "FeaturesRelations" });
            return _mapper.Map<List<FeaturesRequest>>(data);
        }
    }
}
