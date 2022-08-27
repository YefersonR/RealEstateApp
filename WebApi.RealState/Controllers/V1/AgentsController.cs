using Core.Application.Inferfaces.Service;
using Core.Application.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Feactures.Estates.Queries.GetEstateByCode;

namespace WebApi.RealState.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Controlador de agentes")]
    public class AgentsController : BaseApiController
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly IUserService _userService;
        public AgentsController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar agentes",
            Description = "Obtiene todos los agentes"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _userService.GetAllAgents(new AgentSearchViewModel());
                if (result == null || result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar datos de un agente",
            Description = "Recibe el id de una agente y muestra sus datos"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public IActionResult GetById(string Id)
        {
            try
            {
                var result = _userService.GetUserInfo(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetAgentProperty/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar propiedades de agente",
            Description = "Obtiene las propiedades que posee un agente"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> GetAgentProperty(string Id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllEstatesByAgentIdQuery() { AgentId = Id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("ChangeStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Cambiar estado",
            Description = "Cambia el estado de un agente"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ChangeStatus(string Id, string estado)
        {
            try
            {
                await _userService.ChangeUserState(Id, estado);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
