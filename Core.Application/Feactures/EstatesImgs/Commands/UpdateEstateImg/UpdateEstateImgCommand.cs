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

namespace Core.Application.Feactures.EstatesImgs.Commands.UpdateEstateImg
{
    public class UpdateEstateImgCommand : IRequest<UpdateEstateImgCommandResponse>
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public int EstatesId { get; set; }
    }
    public class UpdateEstateImgCommandHandler : IRequestHandler<UpdateEstateImgCommand, UpdateEstateImgCommandResponse>
    {
        private readonly IEstatesImgRepository _imgsRepository;
        private readonly IMapper _mapper;
        public UpdateEstateImgCommandHandler(IEstatesImgRepository imgsRepository, IMapper mapper)
        {
            _imgsRepository = imgsRepository;
            _mapper = mapper;
        }
        public async Task<UpdateEstateImgCommandResponse> Handle(UpdateEstateImgCommand request, CancellationToken cancellationToken)
        {
            var img = await _imgsRepository.GetByIdAsync(request.Id);

            if (img == null) throw new Exception("Img not found");

            img = _mapper.Map<EstatesImg>(request);

            await _imgsRepository.UpdateAsync(img, img.Id);

            var updateimg= _mapper.Map<UpdateEstateImgCommandResponse>(request);
            return updateimg;
        }
    }
}
