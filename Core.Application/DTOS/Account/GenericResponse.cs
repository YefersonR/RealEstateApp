using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Account
{
    public class GenericResponse
    {
        public bool HasError { get; set; } = false;
        public string Error{ get; set; }

    }
}
