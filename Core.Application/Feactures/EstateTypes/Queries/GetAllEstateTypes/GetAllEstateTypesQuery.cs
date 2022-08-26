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

namespace Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes
{
    public class GetAllEstateTypesQuery : IRequest<List<EstateTypeRequest>>
    {
    }
    public class GetAllEstateTypesQueryHandler : IRequestHandler<GetAllEstateTypesQuery, List<EstateTypeRequest>>
    {
        private readonly IEstateTypesRepository _estateTypesRepository;
        private readonly IMapper _mapper;
        public GetAllEstateTypesQueryHandler(IEstateTypesRepository estateTypesRepository, IMapper mapper)
        {
            _estateTypesRepository = estateTypesRepository;
            _mapper = mapper;
        }
        public async Task<List<EstateTypeRequest>> Handle(GetAllEstateTypesQuery request, CancellationToken cancellationToken)
        {
            var response = await _estateTypesRepository.GetAllAsync();
            return _mapper.Map<List<EstateTypeRequest>>(response);
        }
    }

}
