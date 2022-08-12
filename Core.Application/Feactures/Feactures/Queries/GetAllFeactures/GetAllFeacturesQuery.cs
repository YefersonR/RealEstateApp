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
    public class GetAllFeacturesQuery : IRequest<FeacturesRequest>
    {
    }
    public class GetAllFeacturesQueryHandler : IRequestHandler<GetAllFeacturesQuery, FeacturesRequest>
    {
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public GetAllFeacturesQueryHandler(IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _mapper = mapper;
        }
        public Task<FeacturesRequest> Handle(GetAllFeacturesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
