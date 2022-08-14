using Core.Application.DTOS.Estates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.EstatesImgs.Queries.GetAllEstateImgs
{
    public class GetAllEstateImgQuery : IRequest<List<EstateImgRequest>>
    {
        public string EsteteCode { get; set; }

    }
    public class GetAllEstateImgCommandHandler : IRequestHandler<GetAllEstateImgQuery, List<EstateImgRequest>>
    {
        public Task<List<EstateImgRequest>> Handle(GetAllEstateImgQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
