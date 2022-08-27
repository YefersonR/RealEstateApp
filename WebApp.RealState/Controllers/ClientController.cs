using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes;
using Core.Application.Feactures.Favorites.Commands.CreateFavorite;
using Core.Application.Feactures.Favorites.Commands.DeleteFavoriteById;
using Core.Application.Feactures.Favorites.Queries.GetFavoritesById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WebApp.RealState.Middleware;

namespace WebApp.RealState.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClientController : Controller
    {
        private readonly ValidateUser _validateUser;
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public ClientController(ValidateUser validateUser)
        {
            _validateUser = validateUser;
        }

        public async Task<IActionResult> AddFav(int EstateId)
        {
            CreateFavoriteCommand command = new();
            command.EstateId = EstateId;
            command.UserId = _validateUser.GetUserID();
            
            await Mediator.Send(command);
            return RedirectToRoute(new { Controller = "General", Action = "Index"});
        }

        public async Task<IActionResult> DellFavorite(int FavId, string Route)
        {
            await Mediator.Send(new DeleteFavoriteByIdCommand() { Id = FavId });
            
            if (Route == "Fav")
            {
                return RedirectToRoute(new { Controller = "Client", Action = "Favorites" });
            }
            return RedirectToRoute(new { Controller = "General", Action = "Index" });
        }

        public async Task<IActionResult> Favorites(GetAllEstatesParameters parameters)
        {
            return View(await Mediator.Send(new GetAllEstatesQuery(){ FavUserId = _validateUser.GetUserID(), FavOnly = true }));
        }
    }
}
