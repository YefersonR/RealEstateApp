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
        private readonly IMapper _mapper;
        public GetEstateByIdQueryHandler(IEstatesRepository estatesRepository, IMapper mapper)
        {
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
            var estateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs" });
            var estate = estateList.Where(x => x.Code == Code).FirstOrDefault();
            var estateRequest = _mapper.Map<EstateRequest>(estate);
            return estateRequest;
        }
    }
}
