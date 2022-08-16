using Core.Application.Feactures.Favorites.Commands.CreateFavorite;
using Core.Application.Feactures.Favorites.Commands.DeleteFavoriteById;
using Core.Application.Feactures.Favorites.Queries.GetFavoritesById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace WebApp.RealState.Controllers
{
    public class ClientController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        
        public async Task<IActionResult> AddFav(int EstateId)
        {
            CreateFavoriteCommand command = new();
            command.EstateId = EstateId;
            command.UserId = "3b237ad8-eec7-409c-badd-47fc36ee9f51"; //just a little test 
            
            await Mediator.Send(command);
            return RedirectToRoute(new { Controller = "General", Action = "Index"});
        }

        public async Task<IActionResult> DellFavorite(int FavId, string Route)
        {
            await Mediator.Send(new DeleteFavoriteByIdCommand() { Id = FavId });
            
            if (Route == "Index")
            {
                return RedirectToRoute(new { Controller = "General", Action = "Index" });
            }
            
            return RedirectToRoute(new { Controller = "Client", Action = "Favorites" });
        }

        public async Task<IActionResult> Favorites()
        {
            string User = "3b237ad8-eec7-409c-badd-47fc36ee9f51"; //IdUserActive
            return View(await Mediator.Send(new GetFavoritesByIdQuery() { UserId = User }));
        }
    }
}
