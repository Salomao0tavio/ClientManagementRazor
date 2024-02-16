using B2B.Application.DTOs.Response;
using B2B.Application.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using B2B.Application.Interfaces;

namespace B2B.API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {
        private ILoginService _identityService;

        public UserController(ILoginService identityService) =>
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