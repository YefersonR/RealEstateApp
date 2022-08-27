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
    }
    public class CreateEstateImgCommandHandler : IRequestHandler<CreateEstateImgCommand, int>
    {
        private readonly IEstatesImgRepository _imgsRepository;
        private readonly IMapper _mapper;
        public CreateEstateImgCommandHandler(IEstatesImgRepository imgsRepository, IMapper mapper)
        {
            _imgsRepository = imgsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEstateImgCommand request, CancellationToken cancellationToken)
        {
            var Img = _mapper.Map<EstatesImg>(request);
            await _imgsRepository.AddAsync(Img);
            return Img.Id;
        }
    }
}
