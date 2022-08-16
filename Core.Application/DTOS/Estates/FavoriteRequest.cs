using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Estates
{
    public class FavoriteRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EstateId { get; set; }
        public EstateRequest Estates { get; set; }
    }
}
