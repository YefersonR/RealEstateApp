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

namespace Core.Application.Feactures.Estates.Commands.DeleteEstateById
{
    public class DeleteEstateByCodeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteEstateByCodeCommandHandler : IRequestHandler<DeleteEstateByCodeCommand, int>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IMapper _mapper;
        private readonly IFeaturesRelationsRepository _featuresRelationsRepository;
        public DeleteEstateByCodeCommandHandler(IEstatesRepository estatesRepository, IMapper mapper, IFeaturesRelationsRepository featuresRelationsRepository)
        {
            _estatesRepository = estatesRepository;
            _mapper = mapper;
            _featuresRelationsRepository = featuresRelationsRepository;
        }
        public async Task<int> Handle(DeleteEstateByCodeCommand request, CancellationToken cancellationToken)
        {
            var estate = await _estatesRepository.GetByIdAsync(request.Id);

            if (estate == null) throw new Exception("Estate not found");

            await _estatesRepository.DeleteAsync(estate);

            return request.Id;
        }
    }
}
