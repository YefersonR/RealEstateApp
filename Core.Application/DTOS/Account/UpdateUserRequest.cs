using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOS.Account
{
    public class UpdateUserRequest : RegisterRequest
    {
        public string Id { get; set; }
    }
}
