using Core.Application.DTOS.Estates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Feactures.EstatesImgs.Queries.GetAllEstateImg
{
    public class GetAllEstateImgParameters 
    {
        public string EsteteCode { get; set; }
    }

}
