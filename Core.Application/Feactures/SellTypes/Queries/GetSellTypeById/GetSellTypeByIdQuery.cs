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

namespace Core.Application.Feactures.SellTypes.Queries.GetSellTypeById
{
    public class GetSellTypeByIdQuery : IRequest<SellTypeRequest>
    {
        public int Id
        {
            get; set;
        }
    }
    public class GetSellTypeByIdQueryHandler : IRequestHandler<GetSellTypeByIdQuery, SellTypeRequest>
    {
        private readonly ISellTypesRepository _sellTypesRepository;
        private readonly IMapper _mapper;
        public GetSellTypeByIdQueryHandler(ISellTypesRepository sellTypesRepository, IMapper mapper)
        {
            _sellTypesRepository = sellTypesRepository;
            _mapper = mapper;
        }
        public async Task<SellTypeRequest> Handle(GetSellTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var estate = await GetWithIncludeById(request.Id);
            return estate;
        }
        public async Task<SellTypeRequest> GetWithIncludeById(int Id)
        {
            var estate = await _sellTypesRepository.GetByIdAsync(Id);
            var estateRequest = _mapper.Map<SellTypeRequest>(estate);
            return estateRequest;
        }
    }
}
