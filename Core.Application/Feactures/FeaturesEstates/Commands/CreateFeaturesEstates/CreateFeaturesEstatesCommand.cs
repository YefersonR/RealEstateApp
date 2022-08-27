using AutoMapper;
using Core.Application.Inferfaces.Service;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Feactures.Commands.CreateFeaturesEstates
{
    public class CreateFeaturesEstatesCommand : IRequest<int>
    {
        public string Code { get; set; }
        public int FeaturedId { get; set; }
        public int EstateId { get; set; }


    }
    public class CreateFeaturesEstatesCommandHandler : IRequestHandler<CreateFeaturesEstatesCommand, int>
    {
        private readonly IFeaturesRelationsRepository _featuresRelationRepository;
        private readonly IMapper _mapper;
        private readonly IEstatesRepository _estatesRepository;
        public CreateFeaturesEstatesCommandHandler(IFeaturesRelationsRepository featuresRelationRepository, IMapper mapper, IEstatesRepository estatesRepository)
        {
            _estatesRepository = estatesRepository;
            _featuresRelationRepository = featuresRelationRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateFeaturesEstatesCommand request, CancellationToken cancellationToken)
        {
            var estate = await _estatesRepository.GetAllAsync();
            request.EstateId = estate.Where(x => x.Code == request.Code).FirstOrDefault().Id;
            FeaturesRelations feacture = new();
            feacture.FeatureId = request.FeaturedId;
            feacture.EstateId = request.EstateId;
            await _featuresRelationRepository.AddAsync(feacture);
            return feacture.Id;
        }
    }
}
