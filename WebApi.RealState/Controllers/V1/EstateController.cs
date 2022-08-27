using Core.Application.DTOS.Estates;
using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Feactures.Estates.Queries.GetEstateByCode;
using Core.Application.Feactures.Estates.Queries.GetEstateById;
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
    [SwaggerTag("Controlador de propiedades")]
    public class EstateController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar propiedades",
            Description = "Obtiene los datos de todas las propiedades"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> List([FromQuery] GetAllEstatesParameters parameters)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllEstatesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar datos de una propiedad",
            Description = "Recibe el id de una propiedad y muestra sus datos"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetEstateByIdQuery { Id = Id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetByCode/{Code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar datos de una propiedad",
            Description = "Recibe el codigo de una propiedad y muestra sus datos"
            )]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> GetByCode(string Code)
        {
            try
            {
                return Ok(await Mediator.Send(new GetEstateByCodeQuery() { Code = Code }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
