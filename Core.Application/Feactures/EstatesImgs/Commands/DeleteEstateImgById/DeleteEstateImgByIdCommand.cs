using AutoMapper;
using Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.EstatesImgs.Commands.DeleteEstateImgById
{
    public class DeleteEstateImgByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteEstateImgByIdCommandHandler : IRequestHandler<DeleteEstateImgByIdCommand, int>
    {
        private readonly IEstatesImgRepository _imgsRepository;
        private readonly IMapper _mapper;
        public DeleteEstateImgByIdCommandHandler(IEstatesImgRepository imgsRepository, IMapper mapper)
        {
            _imgsRepository = imgsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteEstateImgByIdCommand request, CancellationToken cancellationToken)
        {
            var img = await _imgsRepository.GetByIdAsync(request.Id);

            if (img == null) throw new Exception("Img not found");

            await _imgsRepository.DeleteAsync(img);

            return request.Id;
        }
    }
}
