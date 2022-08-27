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

namespace Core.Application.Feactures.SellTypes.Commands.CreateSellType
{
    /// <summary>
    /// Parametros para la Creacion de un tipo de venta
    /// </summary>
    public class CreateSellTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateSellTypeCommandHandler : IRequestHandler<CreateSellTypeCommand, int>
    {
        private readonly ISellTypesRepository _sellTypesRepository;
        private readonly IMapper _mapper;
        public CreateSellTypeCommandHandler(ISellTypesRepository sellTypesRepository,IMapper mapper)
        {
            _sellTypesRepository = sellTypesRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSellTypeCommand request, CancellationToken cancellationToken)
        {
            var sellType = _mapper.Map<SellType>(request);
            await _sellTypesRepository.AddAsync(sellType);
            return sellType.Id;
        }
    }
}
