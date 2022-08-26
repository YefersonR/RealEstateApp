using Core.Application.DTOS.Account;
using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Feactures.Estates.Queries.GetEstateByCode;
using Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes;
using Core.Application.Helpers;
using Core.Application.Inferfaces.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WebApp.RealState.Middleware;

namespace WebApp.RealState.Controllers
{
    public class GeneralController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly ValidateUser _validateUser;
        private readonly IUserService _userService;

        public GeneralController(ValidateUser validateUser, IUserService userService)
        {
            _userService = userService;
            _validateUser = validateUser;

        }
        public async Task<IActionResult> Index(GetAllEstatesParameters parameters)
        {
            ViewBag.IsLoggin = _validateUser.HasUser();
            ViewBag.EstateTypes = await Mediator.Send(new GetAllEstateTypesQuery());
            if (_validateUser.HasUser())
            {
                return View(await Mediator.Send(new GetAllEstatesQuery()
                {
                    MaxPrice = parameters.MaxPrice,
                    MinPrice = parameters.MinPrice,
                    Rooms = parameters.Rooms,
                    Toilets = parameters.Toilets,
                    SellTypeId = parameters.SellTypeId,
                    FavUserId = _validateUser.GetUserID()
                }));
            }
            return View(await Mediator.Send(new GetAllEstatesQuery()
                        { MaxPrice = parameters.MaxPrice, MinPrice = parameters.MinPrice, Rooms = parameters.Rooms,
                            Toilets = parameters.Toilets, SellTypeId = parameters.SellTypeId, FavUserId = parameters.FavUserId }));
        }

        public async Task<IActionResult> Estate(string code)
        {
            return View(await Mediator.Send(new GetEstateByCodeQuery() { Code = code }));
        }
    }
}
