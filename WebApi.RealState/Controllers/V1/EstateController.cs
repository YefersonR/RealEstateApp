using Core.Application.DTOS.Estates;
using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Feactures.Estates.Queries.GetEstateByCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RealState.Controllers.V1
{
    [ApiVersion("1.0")]
    public class EstateController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(EstateRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List([FromQuery] GetAllEstatesParameters parameters)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllEstatesQuery()));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int Id)
        {
            try 
            {
                return Ok(await Mediator.Send(new GetEstateByCodeQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetById/{Code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstateRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(string Code)
        {
            try
            {
                return Ok(await Mediator.Send(new GetEstateByCodeQuery() {Code = Code }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
