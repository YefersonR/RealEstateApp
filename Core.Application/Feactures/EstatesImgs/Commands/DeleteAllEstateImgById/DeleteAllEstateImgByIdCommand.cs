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
    public class DeleteAllEstateImgByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteAllEstateImgByIdCommandHandler : IRequestHandler<DeleteAllEstateImgByIdCommand, int>
    {
        private readonly IEstatesImgRepository _imgsRepository;
        private readonly IMapper _mapper;
        public DeleteAllEstateImgByIdCommandHandler(IEstatesImgRepository imgsRepository, IMapper mapper)
        {
            _imgsRepository = imgsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteAllEstateImgByIdCommand request, CancellationToken cancellationToken)
        {
            var img = await _imgsRepository.GetAllAsync();
            img = img.Where(x => x.EstatesId == request.Id).ToList();

            if (img == null) throw new Exception("Img not found");

            foreach(var item in img)
            {
                await _imgsRepository.DeleteAsync(item);
            }
            return request.Id;
        }
    }
}
