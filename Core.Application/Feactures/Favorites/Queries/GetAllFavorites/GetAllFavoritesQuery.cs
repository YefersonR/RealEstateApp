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

namespace Core.Application.Feactures.Favorites.Queries.GetAllFavorites
{
    public class GetAllFavoritesQuery : IRequest<List<FavoriteRequest>>
    {
        public string EsteteCode { get; set; }
    }
    public class GetAllFavoritesQueryHandler : IRequestHandler<GetAllFavoritesQuery, List<FavoriteRequest>>
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IMapper _mapper;

        public GetAllFavoritesQueryHandler(IFavoritesRepository favoritesRepository, IMapper mapper)
        {
            _favoritesRepository = favoritesRepository;
            _mapper = mapper;
        }

        public Task<List<FavoriteRequest>> Handle(GetAllFavoritesQuery request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}
