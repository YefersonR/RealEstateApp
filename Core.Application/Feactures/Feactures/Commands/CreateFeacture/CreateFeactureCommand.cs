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

namespace Core.Application.Feactures.Feactures.Commands.CreateFeacture
{
    /// <summary>
    /// Parametros para la Creacion de una mejora
    /// </summary>
    public class CreateFeactureCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
    public class CreateFeactureCommandHandler : IRequestHandler<CreateFeactureCommand, int>
    {
        private readonly IFeaturesRepository _featuresRepository;
        private readonly IMapper _mapper;
        public CreateFeactureCommandHandler(IFeaturesRepository featuresRepository, IMapper mapper)
        {
            _featuresRepository = featuresRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateFeactureCommand request, CancellationToken cancellationToken)
        {
            var feacture = _mapper.Map<Feature>(request);
            await _featuresRepository.AddAsync(feacture);
            return feacture.Id;
        }
    }
}
