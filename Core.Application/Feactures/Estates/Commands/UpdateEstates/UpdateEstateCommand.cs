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
        public string ImageProfile { get; set; }
        public int EstateTypesId { get; set; }
        public string AgentId { get; set; }
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
