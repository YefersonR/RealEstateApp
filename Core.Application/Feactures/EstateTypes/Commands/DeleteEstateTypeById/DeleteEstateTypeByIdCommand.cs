using AutoMapper;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.EstateTypes.Commands.DeleteEstateTypeById
{
    /// <summary>
    /// Parametros para la eliminacion de un tipo de propiedad
    /// </summary>
    public class DeleteEstateTypeByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteEstateTypeByIdCommandHandler : IRequestHandler<DeleteEstateTypeByIdCommand, int>
    {
        private readonly IEstateTypesRepository _estateTypesRepository;
        private readonly IMapper _mapper;
        public DeleteEstateTypeByIdCommandHandler(IEstateTypesRepository estateTypesRepository, IMapper mapper)
        {
            _estateTypesRepository = estateTypesRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteEstateTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var estateType = await _estateTypesRepository.GetByIdAsync(request.Id);

            if (estateType == null) throw new Exception("Estate Type not found");

            await _estateTypesRepository.DeleteAsync(estateType);

            return request.Id;
        }
    }
}
