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
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Application.Feactures.Estates.Commands.CreateEstates
{
    /// <summary>
    /// Parametros para la creacion de una propiedad
    /// </summary>
    public class CreateEstateCommand : IRequest<string>
    {
        [SwaggerParameter(Description = "El Codigo de la propiedad")]
        public string Code { get; set; }
        
        [SwaggerParameter(Description = "Precio por el que se alquila la propiedad")]
        public double Price { get; set; }
        
        [SwaggerParameter(Description = "Area en la que se encuentra la propiedad")]
        public double Area { get; set; }
        
        [SwaggerParameter(Description = "Cantidad de habitaciones que tiene la propiedad")]
        public int Rooms { get; set; }
        
        [SwaggerParameter(Description = "Cantidad de baños que tiene la propiedad")]
        public int Toilets { get; set; }
        
        [SwaggerParameter(Description = "Breve descripcion de la propiedad")]
        public string Description { get; set; }
        public string ImageProfile { get; set; }

        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int EstateTypesId { get; set; }
        
        [SwaggerParameter(Description = "Id del agente dueño de la propiedad")]
        public string AgentId { get; set; }
        
        [SwaggerParameter(Description = "Id del tipo de venta que tiene la propiedad")]
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
