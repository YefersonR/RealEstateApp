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

namespace Core.Application.Feactures.Feactures.Queries.GetFeactureById
{
    public class GetFeactureByIdQuery : IRequest<FeaturesRequest>
    {
        public int Id
        {
            get; set;
        }
    }
    public class GetFeactureByIdQueryHandler : IRequestHandler<GetFeactureByIdQuery, FeaturesRequest>
    {
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public GetFeactureByIdQueryHandler(IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _mapper = mapper;
        }
        public async Task<FeaturesRequest> Handle(GetFeactureByIdQuery request, CancellationToken cancellationToken)
        {
            var estate = await GetWithIncludeById(request.Id);
            return estate;
        }
        public async Task<FeaturesRequest> GetWithIncludeById(int Id)
        {
            var estate = await _featuresRepository.GetByIdAsync(Id);
            var estateRequest = _mapper.Map<FeaturesRequest>(estate);
            return estateRequest;
        }
    }
}
