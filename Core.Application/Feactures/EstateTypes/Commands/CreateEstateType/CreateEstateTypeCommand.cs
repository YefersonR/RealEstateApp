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

namespace Core.Application.Feactures.EstateTypes.Commands.CreateEstateType
{
    public class CreateEstateTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateEstateTypeCommandHandler : IRequestHandler<CreateEstateTypeCommand, int>
    {
        private readonly IEstateTypesRepository _estateTypesRepository;
        private readonly IMapper _mapper;
        public CreateEstateTypeCommandHandler(IEstateTypesRepository estateTypesRepository, IMapper mapper)
        {
            _estateTypesRepository = estateTypesRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEstateTypeCommand request, CancellationToken cancellationToken)
        {
            var estateType = _mapper.Map<EstateType>(request);
            await _estateTypesRepository.AddAsync(estateType);
            return estateType.Id;
        }
    }
}
