using Core.Application.DTOS.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Inferfaces.Service
{
    public interface IEmailService
    {
        Task Send(EmailRequest request);
    }
}
