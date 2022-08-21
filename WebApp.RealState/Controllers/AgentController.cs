﻿using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Inferfaces.Service;
using Core.Application.ViewModels.User;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Feactures.Estates.Queries.GetEstateByCode;
using Core.Application.Feactures.Estates.Commands.CreateEstates;
using Core.Application.Feactures.Estates.Commands.UpdateEstates;
using Core.Application.Feactures.Estates.Commands.DeleteEstateById;
using Core.Application.Feactures.SellTypes.Queries.GetAllSellTypes;
using Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes;

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
        /*public IActionResult Index()
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
        }*/

        public async Task<IActionResult> Estates(string AgentId)
        {
            ViewBag.SellTypes = await Mediator.Send(new GetAllSellTypesQuery());
            ViewBag.SellTypes = await Mediator.Send(new GetAllEstateTypesQuery());
            return View(await Mediator.Send(new GetAllEstatesByAgentIdQuery() { AgentId = AgentId }));
        }

        [HttpPost]
        public async Task<IActionResult> Estates(CreateEstateCommand command)
        {
            command.Code = Guid.NewGuid().ToString(); //.Substring(command)
            return View(await Mediator.Send(command));
        }

        public async Task<IActionResult> EditEstate(string Code)
        {
            await Mediator.Send(new GetEstateByCodeQuery() { Code = Code }); //send to edit
            return RedirectToRoute(new { Controller = "Agent", Action = "Estates" });
        }

        [HttpPost]
        public async Task<IActionResult> EditEstate(UpdateEstateCommand command)
        {
            //Arreglar lo de que code sea el ID en Estates
            await Mediator.Send(command);
            return RedirectToRoute(new { Controller = "Agent", Action = "Estates" });
        }

        public async Task<IActionResult> DeleteEstate(string Code)
        {
            await Mediator.Send(new DeleteEstateByCodeCommand() { Code = Code });
            return RedirectToRoute(new { Controller = "Agent", Action = "Estates" });
        }
    }
}
