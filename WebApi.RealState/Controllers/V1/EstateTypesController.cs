using Core.Application.DTOS.Estates;
using Core.Application.Feactures.EstateTypes.Commands.CreateEstateType;
using Core.Application.Feactures.EstateTypes.Commands.DeleteEstateTypeById;
using Core.Application.Feactures.EstateTypes.Commands.UpdateEstateType;
using Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes;
using Core.Application.Feactures.EstateTypes.Queries.GetEstateTypeById;
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
    [SwaggerTag("Mantenimiento de tipo de propiedades")]
    public class EstateTypesController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Crear tipo de propiedad",
            Description = "Recibe los parametros para crear un tipo de propiedad"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateEstateTypeCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateTypeRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Actualizar un tipo de propiedad",
            Description = "Recibe los parametros para actualizar un tipo de propiedad"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateEstateTypeCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateTypeRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar tipos de propiedades",
            Description = "Obtiene los datos de todas los tipos de propiedades"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> List()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllEstateTypesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateTypeRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar datos de un tipo de propiedad",
            Description = "Recibe el id de un tipo de propiedad y muestra sus datos"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetEstateTypeByIdQuery { Id = Id }));
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
            Summary = "Eliminar mejora",
            Description = "Recibe el id de un tipo de propiedad y lo elimina"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await Mediator.Send(new DeleteEstateTypeByIdCommand { Id = Id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
