using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Feactures.Estates.Queries.GetEstateByCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace WebApp.RealState.Controllers
{
    public class GeneralController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IActionResult> Index(GetAllEstatesParameters parameters)
        {
            return View(await Mediator.Send(new GetAllEstatesQuery() 
                        { Price = parameters.Price, Rooms = parameters.Rooms, Toilets = parameters.Toilets, SellTypeId = parameters.SellTypeId }));
        }

        public async Task<IActionResult> Estate(string code)
        {
            code = "474747";
            return View(await Mediator.Send(new GetEstateByCodeQuery() { Code = code }));
        }
    }
}
