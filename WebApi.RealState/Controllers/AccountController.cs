using Core.Application.DTOS.Account;
using Core.Application.Enum;
using Core.Application.Inferfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using WebApi.RealState.Controllers;

namespace WebApi.RealEstate.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [SwaggerTag("Controlador de usuarios")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Iniciar Session",
            Description = "Comprueba los datos del usuario y devuelve el token de authenticacion"
            )]
        public async Task<IActionResult> Authentication(AuthenticationRequest request)
        {
            return Ok(await _accountService.Authentication(request));
        }
       
        [HttpPost("registerDev")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Registro desarrolladores",
            Description = "Crea un nuevo usuario con el rol desarrollador"
            )]
        public async Task<IActionResult> RegisterDev(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            request.UserType = Roles.Desarrollador.ToString();
            return Ok(await _accountService.Register(request, origin));
        }
        
        [HttpPost("registerAdmin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Registro administradores",
            Description = "Crea un nuevo usuario con el rol administrador"
            )]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            request.UserType = Roles.Administrador.ToString();
            return Ok(await _accountService.Register(request, origin));
        }
    }
}
