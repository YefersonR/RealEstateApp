using AutoMapper;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Favorites.Commands.DeleteFavoriteById
{
    /// <summary>
    /// Parametros para la eliminacion de una propiedad de favoritos
    /// </summary>
    public class DeleteFavoriteByIdCommand: IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteFavoriteByIdCommandHandler : IRequestHandler<DeleteFavoriteByIdCommand,int>
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IMapper _mapper;
        public DeleteFavoriteByIdCommandHandler(IFavoritesRepository favoritesRepository, IMapper mapper)
        {
            _favoritesRepository = favoritesRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteFavoriteByIdCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _favoritesRepository.GetByIdAsync(request.Id);

            if (favorite == null) throw new Exception("Estate not found");

            await _favoritesRepository.DeleteAsync(favorite);

            return request.Id;
        }
    }
}
