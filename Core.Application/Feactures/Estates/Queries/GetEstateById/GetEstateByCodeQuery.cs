using AutoMapper;
using Core.Application.DTOS.Estates;
using Core.Application.Inferfaces.Service;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Estates.Queries.GetEstateById
{
    public class GetEstateByIdQuery : IRequest<EstateRequest>
    {
        public int Id { get; set; }

    }
    public class GetEstateByIdQueryHandler : IRequestHandler<GetEstateByIdQuery, EstateRequest>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public GetEstateByIdQueryHandler(IEstatesRepository estatesRepository, IFeaturesRepository featuresRepository, IMapper mapper, IUserService userService)
        {
            _featuresRepository = featuresRepository;
            _estatesRepository = estatesRepository;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<EstateRequest> Handle(GetEstateByIdQuery request, CancellationToken cancellationToken)
        {
            var estate = await GetWithIncludeById(request.Id);
            return estate;
        }
        public async Task<EstateRequest> GetWithIncludeById(int Id)
        {
            var estateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs", "FeaturesRelations" });
            var estate = estateList.Where(x => x.Id == Id).FirstOrDefault();
            var estateRequest = _mapper.Map<EstateRequest>(estate);
            estateRequest.FeaturesRelations.ForEach(x => x.Features = _mapper.Map<FeaturesRequest>(_featuresRepository.GetByIdAsync(x.FeatureId).Result));
            estateRequest.Agente = await _userService.GetUserInfo(estate.AgentId);
            return estateRequest;
        }
    }
}
