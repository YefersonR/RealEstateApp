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

namespace Core.Application.Feactures.Estates.Queries.GetAllEstates
{
    public class GetAllEstatesQuery : IRequest<List<EstateRequest>>
    {
        public int? SellTypeId { get; set; }
        public double? Price { get; set; }
        public int? Rooms { get; set; }
        public int? Toilets { get; set; }

    }
    public class GetAllEstatesQueryHandler : IRequestHandler<GetAllEstatesQuery, List<EstateRequest>>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IMapper _mapper;
        public GetAllEstatesQueryHandler(IEstatesRepository estatesRepository, IMapper mapper)
        {
            _estatesRepository = estatesRepository;
            _mapper = mapper;
        }
        public async Task<List<EstateRequest>> Handle(GetAllEstatesQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<GetAllEstatesParameters>(request); 
            var estateList = await GetAllWithFilter(parameters);
            if (estateList == null || estateList.Count == 0) throw new Exception("State not found");
            return estateList;
        }

        public async Task<List<EstateRequest>> GetAllWithFilter(GetAllEstatesParameters parameters)
        {
            var estateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs" });
            if(parameters.Toilets != null)
            {
                estateList = estateList.Where(x => x.Toilets == parameters.Toilets).ToList();
            }
            if (parameters.Price != null)
            {
                estateList = estateList.Where(x => x.Price == parameters.Price).ToList();
            }
            if (parameters.Rooms != null)
            {
                estateList = estateList.Where(x => x.Rooms == parameters.Rooms).ToList();
            }
            var statesRequest = _mapper.Map<List<EstateRequest>>(estateList);
            
            return statesRequest;
        }
    }
}
