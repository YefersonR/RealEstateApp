using AutoMapper;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.Estates.Commands.DeleteEstateById
{
    /// <summary>
    /// Parametro para la eliminacion de una propiedad
    /// </summary>
    public class DeleteEstateByCodeCommand : IRequest<string>
    {
        public string Code { get; set; }
    }
    public class DeleteEstateByCodeCommandHandler : IRequestHandler<DeleteEstateByCodeCommand, string>
    {
        private readonly IEstatesRepository _estatesRepository;
        private readonly IMapper _mapper;

        public DeleteEstateByCodeCommandHandler(IEstatesRepository estatesRepository, IMapper mapper)
        {
            _estatesRepository = estatesRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(DeleteEstateByCodeCommand request, CancellationToken cancellationToken)
        {
            var estate = await _estatesRepository.GetByCodeAsync(request.Code);

            if (estate == null) throw new Exception("Estate not found");

            await _estatesRepository.DeleteAsync(estate);

            return request.Code;
        }
    }
}
