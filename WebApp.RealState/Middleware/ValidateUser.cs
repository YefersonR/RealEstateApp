using Core.Application.DTOS.Account;
using Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealState.Middleware
{
    public class ValidateUser
    {
        private readonly IHttpContextAccessor _httpContext;
        public ValidateUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool HasUser()
        {
            AuthenticationResponse user = _httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");
            return user == null ? false : true;
        }
    }
}
