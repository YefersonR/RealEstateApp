using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Feactures.EstatesImgs.Commands.UpdateEstateImg
{
    public class UpdateEstateImgCommandResponse
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public int EstatesId { get; set; }
    }
}
