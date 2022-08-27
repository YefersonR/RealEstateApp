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
using Core.Application.Feactures.Estates.Queries.GetEstateByCode;
using Core.Application.Feactures.Estates.Commands.CreateEstates;
using Core.Application.Feactures.Estates.Commands.UpdateEstates;
using Core.Application.Feactures.Estates.Commands.DeleteEstateById;
using Core.Application.Feactures.SellTypes.Queries.GetAllSellTypes;
using Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes;
using WebApp.RealState.Middleware;
using Core.Application.Helpers;
using Core.Application.Feactures.Feactures.Queries.GetAllFeactures;
using Microsoft.AspNetCore.Http;
using Core.Application.Feactures.EstatesImgs.Commands.CreateEstateImg;
using Core.Application.Feactures.Feactures.Commands.CreateFeaturesEstates;
using Core.Application.DTOS.Estates;
using Core.Application.Feactures.Feactures.Commands.DeleteAllFeactureById;
using Core.Application.Feactures.EstatesImgs.Commands.DeleteEstateImgById;
using Core.Application.Enum;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.RealState.Controllers
{
    public class AgentController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private readonly IUserService _userService;
        private readonly ValidateUser _validateUser;
        private readonly UploadImages _upload;

        public AgentController(IUserService userService, ValidateUser validateUser)
        {
            _upload = new();
            _validateUser = validateUser;
            _userService = userService;
        }
        public async Task<IActionResult> Index(AgentSearchViewModel vm)
        {
            return View(await _userService.GetAllAgents(vm));
        }
        public async Task<IActionResult> Info(string Id)
        {
            ViewBag.IsLoggin = _validateUser.HasUser();
            return View(await Mediator.Send(new GetAllEstatesQuery() { AgentID = Id}));
        }
        [Authorize(Roles = "Agente")]
        public async Task<IActionResult> MyEstates()
        {
            return View(await Mediator.Send(new GetAllEstatesQuery() { AgentID = _validateUser.GetUserID() }));
        }
        

        public async Task<IActionResult> Estates()
        {
            ViewBag.SellTypes = await Mediator.Send(new GetAllSellTypesQuery());
            ViewBag.EstateTypes = await Mediator.Send(new GetAllEstateTypesQuery());
            ViewBag.Features = await Mediator.Send(new GetAllFeacturesQuery());
            return View(await Mediator.Send(new GetAllEstatesByAgentIdQuery() { AgentId = _validateUser.GetUserID() }));
        }
        [Authorize(Roles = "Agente")]
        [HttpPost]
        public async Task<IActionResult> Estates(CreateEstateCommand command, List<string> Features, List<IFormFile> ImageEstate)
        {
            ViewBag.SellTypes = await Mediator.Send(new GetAllSellTypesQuery());
            ViewBag.EstateTypes = await Mediator.Send(new GetAllEstateTypesQuery());
            ViewBag.Features = await Mediator.Send(new GetAllFeacturesQuery());
            command.AgentId = _validateUser.GetUserID();
            command.Code = Guid.NewGuid().ToString(); //.Substring(command)
            string code = await Mediator.Send(command);
            foreach (var item in Features)
            {
                CreateFeaturesEstatesCommand featureCommand = new();
                featureCommand.Code = code; ;
                featureCommand.FeaturedId = Int32.Parse(item);
                await Mediator.Send(featureCommand);
            }

            foreach (var item in ImageEstate)
            {
                CreateEstateImgCommand imgCommand = new();
                imgCommand.ImgUrl = _upload.UploadFile(item, code, "ImgsEstate");
                imgCommand.Code = code;
                await Mediator.Send(imgCommand);
            }

            return View();
        }
        [Authorize(Roles = "Agente")]
        public async Task<IActionResult> EditEstate(string Code)
        {
            ViewBag.SellTypes = await Mediator.Send(new GetAllSellTypesQuery());
            ViewBag.EstateTypes = await Mediator.Send(new GetAllEstateTypesQuery());
            ViewBag.Features = await Mediator.Send(new GetAllFeacturesQuery());
            var data = await Mediator.Send(new GetEstateByCodeQuery() { Code = Code }); //send to edit
            return View(data);
        }
        [Authorize(Roles = "Agente")]

        [HttpPost]
        public async Task<IActionResult> EditEstate(EstateRequest request, string EstateTypesId, List<string> Features, List<IFormFile> ImageEstate)
        {
            ViewBag.SellTypes = await Mediator.Send(new GetAllSellTypesQuery());
            ViewBag.EstateTypes = await Mediator.Send(new GetAllEstateTypesQuery());
            ViewBag.Features = await Mediator.Send(new GetAllFeacturesQuery());
            var data = await Mediator.Send(new UpdateEstateCommand() { AgentId = request.AgentId, Area = request.Area,
                Code = request.Code, Description = request.Description, Price = request.Price,
                SellTypeId = request.SellTypeId,
                EstateTypeId = request.EstateTypeId,
                Rooms = request.Rooms,
                Toilets = request.Toilets,
            });
            await Mediator.Send(new DeleteAllFeactureByIdCommand() { Id = data.Id });
            foreach (var item in Features)
            {
                CreateFeaturesEstatesCommand featureCommand = new();
                featureCommand.Code = data.Code; ;
                featureCommand.FeaturedId = Int32.Parse(item);
                await Mediator.Send(featureCommand);
            }
            if(ImageEstate != null)
            {
                await Mediator.Send(new DeleteAllEstateImgByIdCommand() { Id = data.Id });
            }
            foreach (var item in ImageEstate)
            {
                CreateEstateImgCommand imgCommand = new();
                imgCommand.ImgUrl = _upload.UploadFile(item, data.Code, "ImgsEstate");
                imgCommand.Code = data.Code;
                await Mediator.Send(imgCommand);
            }
            return View();
        }

        [Authorize(Roles = "Agente")]
        public async Task<IActionResult> DeleteEstate(int Id)
        {
            await Mediator.Send(new DeleteEstateByCodeCommand() { Id = Id });
            return RedirectToRoute(new { Controller = "Agent", Action = "Estates" });
        }

        [Authorize(Roles = "Agente")]
        public async Task<IActionResult> MyProfile()
        {
            UserSaveViewModel vm = await _userService.GetUserInfo(_validateUser.GetUserID());
            return View(vm);
        }

        [Authorize(Roles = "Agente")]
        [HttpPost]
        public async Task<IActionResult> MyProfile(UserSaveViewModel vm)
        {
            UserSaveViewModel OldImg = await _userService.GetUserInfo(_validateUser.GetUserID());
            if (vm.File != null)
            {
                vm.ImageProfile = _upload.UploadFile(vm.File, vm.Id, "ImgProfile", OldImg.ImageProfile, true);
            }
            if(vm.File == null)
            {
                vm.ImageProfile = OldImg.ImageProfile;
            }
            await _userService.UpdateUser(_validateUser.GetUserID(), vm);
            return View(vm);
        }
    }
}
