using ApiTemplate.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenerateTokenResponse))]
        public async Task<IActionResult> CreateToken(GenerateTokenRequest generateTokenCommand)
        {
            var response = await _mediator.Send(generateTokenCommand);

            return Ok(response);
        }     

    }
}
