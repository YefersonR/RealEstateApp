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

namespace Core.Application.Feactures.EstatesImgs.Commands.CreateEstateImg
{
    /// <summary>
    /// Parametro para la creacion de una imagen de la propiedad 
    /// </summary>
    public class CreateEstateImgCommand : IRequest<int>
    {
        public string Code { get; set; }
        public string ImgUrl { get; set; }
        public int EstatesId { get; set; }
    }
    public class CreateEstateImgCommandHandler : IRequestHandler<CreateEstateImgCommand, int>
    {
        private readonly IEstatesImgRepository _imgsRepository;
        private readonly IMapper _mapper;
        private readonly IEstatesRepository _estatesRepository;
        public CreateEstateImgCommandHandler(IEstatesImgRepository imgsRepository, IMapper mapper, IEstatesRepository estatesRepository)
        {
            _imgsRepository = imgsRepository;
            _mapper = mapper;
            _estatesRepository = estatesRepository;
        }
        public async Task<int> Handle(CreateEstateImgCommand request, CancellationToken cancellationToken)
        {
            var estate = await _estatesRepository.GetAllAsync();
            request.EstatesId = estate.Where(x => x.Code == request.Code).FirstOrDefault().Id;
            EstatesImg img = new();
            img.ImgUrl = request.ImgUrl;
            img.EstatesId = request.EstatesId;
            await _imgsRepository.AddAsync(img);
            return img.Id;
        }
    }
}
