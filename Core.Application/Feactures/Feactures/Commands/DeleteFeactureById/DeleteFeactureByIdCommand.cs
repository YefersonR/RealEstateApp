using AutoMapper;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Feactures.Commands.DeleteFeactureById
{
    /// <summary>
    /// Parametros para la eliminacion de una mejora
    /// </summary>
    public class DeleteFeactureByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteFeactureByIdCommandHandler : IRequestHandler<DeleteFeactureByIdCommand, int>
    {
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public DeleteFeactureByIdCommandHandler(IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteFeactureByIdCommand request, CancellationToken cancellationToken)
        {
             var feature = await _featuresRepository.GetByIdAsync(request.Id);

            if (feature == null) throw new Exception("Estate Type not found");

            await _featuresRepository.DeleteAsync(feature);

            return request.Id;
        }
    }
}
