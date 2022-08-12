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

namespace Core.Application.Feactures.SellTypes.Commands.UpdateSellType
{
    public class UpdateSellTypeCommand : IRequest<UpdateSellTypeCommandResponse>
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
    public class UpdateSellTypeCommandHandler : IRequestHandler<UpdateSellTypeCommand, UpdateSellTypeCommandResponse>
    {
        private readonly ISellTypesRepository _sellTypesRepository;
        private readonly IMapper _mapper;
        public UpdateSellTypeCommandHandler(ISellTypesRepository sellTypesRepository, IMapper mapper)
        {
            _sellTypesRepository = sellTypesRepository;
            _mapper = mapper;
        }
        public async Task<UpdateSellTypeCommandResponse> Handle(UpdateSellTypeCommand request, CancellationToken cancellationToken)
        {
            var sellType = await _sellTypesRepository.GetByIdAsync(request.Id);

            if (sellType == null) throw new Exception("Feacture not found");

            sellType = _mapper.Map<SellType>(request);

            await _sellTypesRepository.UpdateAsync(sellType, sellType.Id);

            var updateState = _mapper.Map<UpdateSellTypeCommandResponse>(request);
            return updateState;
        }
    }
}
