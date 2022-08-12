using AutoMapper;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Favorites.Commands.CreateFavorite
{
    public class CreateFavoriteCommand: IRequest<int>
    {
        public string UserId { get; set; }
        public int EstateId { get; set; }
    }
    public class CreateFavoriteCommadHandler : IRequestHandler<CreateFavoriteCommand, int>
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IMapper _mapper;
        public CreateFavoriteCommadHandler(IFavoritesRepository favoritesRepository, IMapper mapper)
        {
            _favoritesRepository = favoritesRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = _mapper.Map<Favorite>(request);
            await _favoritesRepository.AddAsync(favorite);
            return favorite.Id;
        }
    }
}
