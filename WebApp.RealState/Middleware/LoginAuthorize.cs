using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RealEstate.Controllers;

namespace WebApp.RealState.Middleware
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUser _validateUser;
        public LoginAuthorize(ValidateUser validateUser)
        {
            _validateUser = validateUser;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_validateUser.HasUser())
            {
                var controller = (AccountController)context.Controller;
                context.Result = controller.RedirectToAction("Index", "Home");
            }
            else
            {
                await next();
            }
        }
    }
}
