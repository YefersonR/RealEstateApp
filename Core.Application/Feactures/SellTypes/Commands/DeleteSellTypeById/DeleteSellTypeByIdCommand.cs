using AutoMapper;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.SellTypes.Commands.DeleteSellTypeById
{
    /// <summary>
    /// Parametros para la eliminacion de un tipo de venta
    /// </summary>
    public class DeleteSellTypeByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteSellTypeByIdCommandHandler : IRequestHandler<DeleteSellTypeByIdCommand, int>
    {
        private readonly ISellTypesRepository _sellTypesRepository;
        private readonly IMapper _mapper;
        public DeleteSellTypeByIdCommandHandler(ISellTypesRepository sellTypesRepository, IMapper mapper)
        {
            _sellTypesRepository = sellTypesRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteSellTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var feature = await _sellTypesRepository.GetByIdAsync(request.Id);

            if (feature == null) throw new Exception("Estate Type not found");

            await _sellTypesRepository.DeleteAsync(feature);

            return request.Id;
        }
    }
}
