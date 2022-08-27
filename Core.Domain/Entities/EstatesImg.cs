using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class EstatesImg : AuditableBaseEntity
    {
        public string ImgUrl { get; set; }
        public int EstatesId { get; set; }

        public Estate Estates { get; set; }
    }
}
