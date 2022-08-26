using AutoMapper;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Entities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Estates.Commands.CreateEstates
{
    public class CreateEstateCommand : IRequest<string>
    {
        public string Code { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public int Rooms { get; set; }
        public int Toilets { get; set; }
        public string Description { get; set; }
        public string ImageProfile { get; set; }
        public int EstateTypesId { get; set; }
        public string AgentId { get; set; }
        public int SellTypeId { get; set; }
    }
    public class CreateEstateCommandHandler : IRequestHandler<CreateEstateCommand, string>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IMapper _mapper;
        public CreateEstateCommandHandler(IEstatesRepository estatesRepository,IMapper mapper)
        {
            _estatesRepository = estatesRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateEstateCommand request, CancellationToken cancellationToken)
        {
            var Estate = _mapper.Map<Estate>(request);
            await _estatesRepository.AddAsync(Estate);
            return Estate.Code;
        }


    }
}
