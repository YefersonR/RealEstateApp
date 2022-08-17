using Core.Application.DTOS.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.User
{
    public class AgenteViewModel
    {
        public AgentesViewModel agente { get; set; } 
        public List<EstateRequest> Estates{ get; set;}
    }
}
