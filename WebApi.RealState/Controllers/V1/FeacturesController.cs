using Core.Application.DTOS.Estates;
using Core.Application.Feactures.Feactures.Commands.CreateFeacture;
using Core.Application.Feactures.Feactures.Commands.DeleteFeactureById;
using Core.Application.Feactures.Feactures.Commands.UpdateFeacture;
using Core.Application.Feactures.Feactures.Queries.GetAllFeactures;
using Core.Application.Feactures.Feactures.Queries.GetFeactureById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebApi.RealState.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de mejoras")]
    public class FeacturesController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Crear una mejora",
            Description = "Recibe los parametros para crear una mejora"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateFeactureCommand command)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeaturesRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Actualizar una mejora",
            Description = "Recibe los parametros para actualizar una mejora existente"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateFeacturesCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (Id != command.Id)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeaturesRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar Mejoras",
            Description = "Obtiene todas las mejoras"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> List()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllFeacturesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeaturesRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar datos de una mejora",
            Description = "Recibe el id de una mejora y muestra los datos de esa mejora en particular"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetFeactureByIdQuery { Id = Id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Borrar mejora",
            Description = "Recibe el id de una mejora y la elimina"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await Mediator.Send(new DeleteFeactureByIdCommand { Id = Id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
