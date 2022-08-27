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
    public class DeleteFeaturesEstatesByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteFeaturesEstatesByIdCommandHandler : IRequestHandler<DeleteFeaturesEstatesByIdCommand, int>
    {
        private readonly IFeaturesRelationsRepository _featuresRelationRepository;
        private readonly IMapper _mapper;
        public DeleteFeaturesEstatesByIdCommandHandler(IFeaturesRelationsRepository featuresRelationRepository, IMapper mapper)
        {
            _featuresRelationRepository = featuresRelationRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteFeaturesEstatesByIdCommand request, CancellationToken cancellationToken)
        {
             var feature = await _featuresRelationRepository.GetByIdAsync(request.Id);

            if (feature == null) throw new Exception("Estate Type not found");

            await _featuresRelationRepository.DeleteAsync(feature);

            return request.Id;
        }
    }
}
