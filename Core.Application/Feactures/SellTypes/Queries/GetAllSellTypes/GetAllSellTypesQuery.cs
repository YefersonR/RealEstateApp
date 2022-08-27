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

namespace Core.Application.Feactures.SellTypes.Queries.GetAllSellTypes
{
    public class GetAllSellTypesQuery : IRequest<List<SellTypeRequest>>
    {
    }
    public class GetAllSellTypesQueryHandler : IRequestHandler<GetAllSellTypesQuery, List<SellTypeRequest>>
    {
        private readonly ISellTypesRepository _sellTypesRepository;
        private readonly IMapper _mapper;
        public GetAllSellTypesQueryHandler(ISellTypesRepository sellTypesRepository, IMapper mapper)
        {
            _sellTypesRepository = sellTypesRepository;
            _mapper = mapper;
        }
        public async Task<List<SellTypeRequest>> Handle(GetAllSellTypesQuery request, CancellationToken cancellationToken)
        {
            var selltype = await _sellTypesRepository.GetAllWhitIncludes(new List<string> { "Estates" });
            return _mapper.Map<List<SellTypeRequest>>(selltype); 
        }
    }

}
