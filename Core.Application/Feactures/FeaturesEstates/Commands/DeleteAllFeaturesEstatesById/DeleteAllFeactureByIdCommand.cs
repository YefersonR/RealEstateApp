using AutoMapper;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Feactures.Commands.DeleteAllFeactureById
{
    public class DeleteAllFeactureByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteAllFeactureByIdCommandCommandHandler : IRequestHandler<DeleteAllFeactureByIdCommand, int>
    {
        private readonly IFeaturesRelationsRepository _featuresRelationRepository;
        private readonly IMapper _mapper;
        public DeleteAllFeactureByIdCommandCommandHandler(IFeaturesRelationsRepository featuresRelationRepository, IMapper mapper)
        {
            _featuresRelationRepository = featuresRelationRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteAllFeactureByIdCommand request, CancellationToken cancellationToken)
        {
            var feature = await _featuresRelationRepository.GetAllAsync();
            feature = feature.Where(x => x.EstateId == request.Id).ToList();

            if (feature == null) throw new Exception("Estate Type not found");

            foreach (var item in feature)
            {
                await _featuresRelationRepository.DeleteAsync(item);
            }

            return request.Id;
        }
    }
}
