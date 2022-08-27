using Core.Application.DTOS.Estates;
using Core.Application.Feactures.SellTypes.Commands.CreateSellType;
using Core.Application.Feactures.SellTypes.Commands.DeleteSellTypeById;
using Core.Application.Feactures.SellTypes.Commands.UpdateSellType;
using Core.Application.Feactures.SellTypes.Queries.GetAllSellTypes;
using Core.Application.Feactures.SellTypes.Queries.GetSellTypeById;
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
    [SwaggerTag("Mantenimiento de tipo de ventas")]
    public class SellTypesController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Crear tipo de venta",
            Description = "Recibe los parametros para crear un tipo de venta"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateSellTypeCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellTypeRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Actualizar un tipo de venta",
            Description = "Recibe los parametros para actualizar un tipo de venta"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateSellTypeCommand command)
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
            Summary = "Listar tipo de ventas",
            Description = "Obtiene los datos de todas los tipos de ventas"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> List()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllSellTypesQuery()));
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
            Summary = "Listar datos de un tipo de venta",
            Description = "Recibe el id de un tipo de venta y muestra sus datos"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetSellTypeByIdQuery { Id = Id }));
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
            Description = "Recibe el id de un tipo de venta y lo elimina con sus propiedades"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await Mediator.Send(new DeleteSellTypeByIdCommand { Id = Id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
