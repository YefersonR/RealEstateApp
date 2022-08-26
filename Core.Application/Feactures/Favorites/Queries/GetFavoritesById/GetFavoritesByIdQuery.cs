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

namespace Core.Application.Feactures.Favorites.Queries.GetFavoritesById
{
    public class GetFavoritesByIdQuery : IRequest<List<FavoriteRequest>>
    {
        public string UserId { get; set; }
    }
    public class GetAllFavoritesQueryHandler : IRequestHandler<GetFavoritesByIdQuery, List<FavoriteRequest>>
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IEstatesRepository _estatesRepository;
        private readonly IMapper _mapper;

        public GetAllFavoritesQueryHandler(IFavoritesRepository favoritesRepository, IEstatesRepository estatesRepository, IMapper mapper)
        {
            _favoritesRepository = favoritesRepository;
            _estatesRepository = estatesRepository;
            _mapper = mapper;
        }

        public async Task<List<FavoriteRequest>> Handle(GetFavoritesByIdQuery request, CancellationToken cancellationToken)
        {
            var FavoritesList = await _favoritesRepository.GetAllWhitIncludes(new List<string> { "Estates" });
            List<FavoriteRequest> response = new();
            FavoritesList = FavoritesList.Where(x => x.UserId == request.UserId).ToList();
            var EstateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs" });

            foreach (var data in FavoritesList)
            {
                response.Add(_mapper.Map<FavoriteRequest>(EstateList.Find(x => x.Id == data.EstateId)));
            }
            return _mapper.Map<List<FavoriteRequest>>(FavoritesList);
        }
    }
}
