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

namespace Core.Application.Feactures.Estates.Commands.UpdateEstates
{
    public class UpdateEstateCommand: IRequest<UpdateEstateCommandResponse>
    {
        public string Code { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }
        public int Toilets { get; set; }
        public string Description { get; set; }
        public int EstateTypesId { get; set; }
        public int AgentId { get; set; }
        public int SellTypeId { get; set; }
    }
    public class UpdateEstateCommanHandler : IRequestHandler<UpdateEstateCommand, UpdateEstateCommandResponse>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IMapper _mapper;

        public UpdateEstateCommanHandler(IEstatesRepository estatesRepository, IMapper mapper)
        {
            _estatesRepository = estatesRepository;
            _mapper = mapper;
        }
        public async Task<UpdateEstateCommandResponse> Handle(UpdateEstateCommand request, CancellationToken cancellationToken)
        {
            var estate = await _estatesRepository.GetByCodeAsync(request.Code);

            if (estate == null) throw new Exception("Estate not found");

            estate = _mapper.Map<Estate>(request);

            await _estatesRepository.Update(estate,estate.Code);

            var updateState = _mapper.Map<UpdateEstateCommandResponse>(request); 
            return updateState;
        }

    }
}
