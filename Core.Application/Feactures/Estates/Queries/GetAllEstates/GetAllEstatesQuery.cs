using AutoMapper;
using Core.Application.DTOS.Estates;
using Core.Application.Inferfaces.Service;
using Core.Application.Interface.Repositories;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
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
        public double? MaxPrice { get; set; }
        public double? MinPrice { get; set; }
        public int? Rooms { get; set; }
        public int? Toilets { get; set; }
        public string? FavUserId { get; set; }
        public bool? FavOnly { get; set; }
        public String? AgentID { get; set; }

    }
    public class GetAllEstatesQueryHandler : IRequestHandler<GetAllEstatesQuery, List<EstateRequest>>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public GetAllEstatesQueryHandler(IEstatesRepository estatesRepository, IMapper mapper, IUserService userService)
        {
            _estatesRepository = estatesRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<EstateRequest>> Handle(GetAllEstatesQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<GetAllEstatesParameters>(request); 
            var estateList = await GetAllWithFilter(parameters);
            //if (estateList == null || estateList.Count == 0) throw new Exception("State not found");
            return estateList;
        }

        public async Task<List<EstateRequest>> GetAllWithFilter(GetAllEstatesParameters parameters)
        {
            var estateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs", "Favorites" });
            

            if(parameters.Toilets != null)
            {
                estateList = estateList.Where(x => x.Toilets == parameters.Toilets).ToList();
            }
            
            if (parameters.Rooms != null)
            {
                estateList = estateList.Where(x => x.Rooms == parameters.Rooms).ToList();
            }

            if (parameters.MaxPrice != null)
            {
                estateList = estateList.Where(x => x.Price <= parameters.MaxPrice).ToList();
            }

            parameters.MinPrice = parameters.MinPrice == null ? 0 : parameters.MinPrice;
            estateList = estateList.Where(x => x.Price >= parameters.MinPrice).ToList();

            if(parameters.FavUserId != null)
            {
                List<Estate> getFavs = new();
                List<EstateRequest> estateRequests = new();
                foreach (var data in estateList)
                {
                    var fav = data.Favorites.Where(x => x.UserId == parameters.FavUserId).FirstOrDefault();
                    if (fav != null)
                    {
                        estateRequests.Add(_mapper.Map<EstateRequest>(data));
                        estateRequests.Last().FavoriteId = fav.Id;
                    }
                    if(fav == null)
                    {
                        estateRequests.Add(_mapper.Map<EstateRequest>(data));
                    }
                }
                if (parameters.FavOnly == true)
                {
                    estateRequests = estateRequests.Where(x => x.FavoriteId != null && x.FavoriteId > 0).ToList();
                }
                return estateRequests;
            }
            var estateResponse = _mapper.Map<List<EstateRequest>>(estateList);
            if (parameters.AgentID != null)
            {
                estateResponse = estateResponse.Where(x => x.AgentId == parameters.AgentID).ToList();
                int i = 0;
                foreach(var data in estateResponse)
                {
                    estateResponse[i].Agente = _mapper.Map<AgentesViewModel>(await _userService.GetUserInfo(parameters.AgentID));
                    i++;
                }
            }
            return estateResponse;
        }
    }
}
