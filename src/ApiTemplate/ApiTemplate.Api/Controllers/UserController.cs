using ApiTemplate.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(UserAddCommand userAddCommand)
        {
            var response = await _mediator.Send(userAddCommand);

            return Created($"/get/{response.ToString()}", null);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserListCommandRespose>))]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new UserListCommand());

            return Ok(response);
        }

    }
}
