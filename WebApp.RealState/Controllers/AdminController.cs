using Core.Application.Feactures.Estates.Commands.CreateEstates;
using Core.Application.Feactures.EstateTypes.Commands.CreateEstateType;
using Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes;
using Core.Application.Feactures.Feactures.Commands.CreateFeacture;
using Core.Application.Feactures.Feactures.Commands.DeleteFeactureById;
using Core.Application.Feactures.Feactures.Commands.UpdateFeacture;
using Core.Application.Feactures.Feactures.Queries.GetAllFeactures;
using Core.Application.Feactures.Feactures.Queries.GetFeactureById;
using Core.Application.Feactures.SellTypes.Commands.CreateSellType;
using Core.Application.Feactures.SellTypes.Commands.DeleteSellTypeById;
using Core.Application.Feactures.SellTypes.Commands.UpdateSellType;
using Core.Application.Feactures.SellTypes.Queries.GetAllSellTypes;
using Core.Application.Feactures.SellTypes.Queries.GetSellTypeById;
using Core.Application.Inferfaces.Service;
using Core.Application.ViewModels.AdminPanel;
using Core.Application.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace WebApp.RealState.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<IActionResult> SellTypes()
        {
            return View(await Mediator.Send(new GetAllSellTypesQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> SellTypes(SaveSellTypeViewModel vm)
        {
            await Mediator.Send(new CreateSellTypeCommand() { Description = vm.Description, Name = vm.Name });
            return View(await Mediator.Send(new GetAllSellTypesQuery()));
        }

        public async Task<IActionResult> EditSellType(int Id)
        {
            return View(await Mediator.Send(new GetSellTypeByIdQuery() { Id = Id }));
        }

        [HttpPost]
        public async Task<IActionResult> EditSellType(UpdateSellTypeCommand command)
        {
            return View(await Mediator.Send(command));
        }

        public async Task<IActionResult> DellSellType(int Id)
        {
            await Mediator.Send(new DeleteSellTypeByIdCommand() { Id = Id });
            return RedirectToRoute(new { Controller = "Admin", Action = "SellTypes" });
        }

        public async Task<IActionResult> Features()
        {
            return View(await Mediator.Send(new GetAllFeacturesQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Features(SaveFeaturesViewModel vm)
        {
            await Mediator.Send(new CreateFeactureCommand() { Description = vm.Description, Name = vm.Name });
            return View(await Mediator.Send(new GetAllFeacturesQuery()));
        }

        public async Task<IActionResult> EditFeatures(int Id)
        {
            return View(await Mediator.Send(new GetFeactureByIdQuery() { Id = Id }));
        }

        [HttpPost]
        public async Task<IActionResult> EditFeatures(UpdateFeacturesCommand command)
        {
            return View(await Mediator.Send(command));
        }

        public async Task<IActionResult> DellFeatures(int Id)
        {
            await Mediator.Send(new DeleteFeactureByIdCommand() { Id = Id });
            return RedirectToRoute(new { Controller = "Admin", Action = "SellTypes" });
        }

        [HttpPost]
        public async Task<IActionResult> EstateType(SaveEstateTypeViewModel vm)
        {
            await Mediator.Send(new CreateEstateTypeCommand() { Description = vm.Description, Name = vm.Name });
            return View(await Mediator.Send(new GetAllEstateTypesQuery()));
        }
        
        public async Task<IActionResult> EstateType()
        {
            return View(await Mediator.Send(new GetAllEstateTypesQuery()));
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Agent(AgentSearchViewModel vm)
        {
            return View(await _userService.GetAllAgents(vm));
        }

        public async Task<IActionResult> AgentActive(string Id, bool ActualEstate)
        {
            if(ActualEstate == true)
            {
                await _userService.ChangeUserState(Id, "false");
            }
            if (ActualEstate == false)
            {
                await _userService.ChangeUserState(Id, "true");
            }
            return RedirectToRoute(new {Controller = "Admin", Action = "Agent" });
        }

    }
}