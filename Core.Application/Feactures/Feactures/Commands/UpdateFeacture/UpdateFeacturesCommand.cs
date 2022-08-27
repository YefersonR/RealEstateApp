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

namespace Core.Application.Feactures.Feactures.Commands.UpdateFeacture
{
    /// <summary>
    /// Parametros para la actualizacion de una mejora
    /// </summary>
    public class UpdateFeacturesCommand : IRequest<UpdateFeacturesCommandResponse>
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
    public class UpdateFeacturesCommandHandler : IRequestHandler<UpdateFeacturesCommand, UpdateFeacturesCommandResponse>
    {
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public UpdateFeacturesCommandHandler(IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _mapper = mapper;
        }
        public async Task<UpdateFeacturesCommandResponse> Handle(UpdateFeacturesCommand request, CancellationToken cancellationToken)
        {
            var feacture = await _featuresRepository.GetByIdAsync(request.Id);

            if (feacture == null) throw new Exception("Feacture not found");

            feacture = _mapper.Map<Feature>(request);

            await _featuresRepository.UpdateAsync(feacture, feacture.Id);

            var updateState = _mapper.Map<UpdateFeacturesCommandResponse>(request);
            return updateState;
        }
    }
}
