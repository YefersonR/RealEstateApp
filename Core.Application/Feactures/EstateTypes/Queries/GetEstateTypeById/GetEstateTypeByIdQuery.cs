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

namespace Core.Application.Feactures.EstateTypes.Queries.GetEstateTypeById
{
    public class GetEstateTypeByIdQuery : IRequest<EstateTypeRequest>
    {
        public int Id
        {
            get; set;
        }
    }
    public class GetEstateTypeByIdQueryHandler : IRequestHandler<GetEstateTypeByIdQuery, EstateTypeRequest>
    {
        private readonly IEstateTypesRepository _estateTypesRepository;
        private readonly IMapper _mapper;
        public GetEstateTypeByIdQueryHandler(IEstateTypesRepository estateTypesRepository, IMapper mapper)
        {
            _estateTypesRepository = estateTypesRepository;
            _mapper = mapper;
        }
        public async Task<EstateTypeRequest> Handle(GetEstateTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var estate = await GetWithIncludeById(request.Id);
            return estate;
        }
        public async Task<EstateTypeRequest> GetWithIncludeById(int Id)
        {
            var estate = await _estateTypesRepository.GetByIdAsync(Id);
            var estateRequest = _mapper.Map<EstateTypeRequest>(estate);
            return estateRequest;
        }
    }

}
