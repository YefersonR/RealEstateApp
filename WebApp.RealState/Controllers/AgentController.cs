using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Inferfaces.Service;
using Core.Application.ViewModels.User;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealState.Controllers
{
    public class AgentController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private readonly IUserService _userService;
        public AgentController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            List<AgentesViewModel> agents = _userService.GetAllAgents();
            return View(agents);
        }
        public async Task<IActionResult> Info(string Id)
        {
            AgenteViewModel agent = new();
            agent.agente = await _userService.GetAgentById(Id);
            var allEstates = await Mediator.Send(new GetAllEstatesQuery());
            agent.Estates = allEstates;
            return View(agent);
        }
    }
}
