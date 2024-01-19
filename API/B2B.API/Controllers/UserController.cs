using Application.B2B.Aplication.Interfaces.Services;
using Application.B2B_Application.DTOs.Request;
using Application.B2B_Application.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace Services.B2B.API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {
        private IIdentityService _identityService;

        public UserController(IIdentityService identityService) =>
            _identityService = identityService;

        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest usuarioLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.Login(usuarioLogin);
            if (resultado.Sucess)
                return Ok(resultado);

            return Unauthorized();
        }
    }
}