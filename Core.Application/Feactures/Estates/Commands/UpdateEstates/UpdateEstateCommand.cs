using AutoMapper;
using Core.Application.DTOS.Estates;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Estates.Commands.UpdateEstates
{
    /// <summary>
    /// Parametros para la actualizacion de una propiedad
    /// </summary>
    public class UpdateEstateCommand: IRequest<UpdateEstateCommandResponse>
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
        public int AgentId { get; set; }
        [SwaggerParameter(Description = "Id del tipo de venta que tiene la propiedad")]
        public int SellTypeId { get; set; }
        public int EstateTypeId { get; set; }
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
            var estateList = await _estatesRepository.GetAllWhitIncludes(new List<string> { "SellTypes", "EstateTypes", "EstatesImgs", "Favorites" });
            var estate = estateList.Where(x => x.Code == request.Code).FirstOrDefault();
            if (estate == null) throw new Exception("Estate not found");
            if(request.Toilets != null)
            {
                estate.Toilets = request.Toilets;
            }
            if (request.Rooms != null)
            {
                estate.Rooms = request.Rooms;
            }
            if (request.Price != null)
            {
                estate.Price = request.Price;
            }
            if (request.EstateTypeId != null)
            {
                estate.EstateTypesId = request.EstateTypeId;
            }
            if (request.Area != null)
            {
                estate.Area = request.Area;
            }
            if (request.Description != null)
            {
                estate.Description = request.Description;
            }
            if (request.SellTypeId != null)
            {
                estate.SellTypeId = request.SellTypeId;
            }
            estate.EstateTypesId = estate.EstateTypes.Id;
            estate.SellTypeId = estate.SellTypes.Id;
            await _estatesRepository.Update(estate,estate.Id);

            var updateState = _mapper.Map<UpdateEstateCommandResponse>(request);
            updateState.Id = estate.Id;
            return updateState;
        }

    }
}
