using AutoMapper;
using Core.Application.DTOS.Estates;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.EstateTypes.Commands.UpdateEstateType
{
    public class UpdateEstateTypeCommand : IRequest<UpdateEstateTypeCommandResponse>
    {
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
     }
    public class UpdateEstateTypeCommandHandler : IRequestHandler<UpdateEstateTypeCommand, UpdateEstateTypeCommandResponse>
    {
        private readonly IEstateTypesRepository _estateTypesRepository;
        private readonly IMapper _mapper;
        public UpdateEstateTypeCommandHandler(IEstateTypesRepository estateTypesRepository, IMapper mapper)
        {
            _estateTypesRepository = estateTypesRepository;
            _mapper = mapper;
        }
        public async Task<UpdateEstateTypeCommandResponse> Handle(UpdateEstateTypeCommand request, CancellationToken cancellationToken)
        {
            var estateType = await _estateTypesRepository.GetByIdAsync(request.Id);

            if (estateType == null) throw new Exception("Estate Type not found");

            estateType = _mapper.Map<EstateType>(request);

            await _estateTypesRepository.UpdateAsync(estateType, estateType.Id);

            var updateState = _mapper.Map<UpdateEstateTypeCommandResponse>(request);
            return updateState;
        }
    }
}
