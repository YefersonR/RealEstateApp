using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Estates
{
    public class EstateImgRequest
    {
        public string ImgUrl { get; set; }
        public int EstatesId { get; set; }
    }
}
