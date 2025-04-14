using ApiTemplate.Application.Commands;
using ApiTemplate.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(UserAddRequest userAddRequest)
        {
            var response = await _mediator.Send(userAddRequest);

            return Created($"/get/{response.ToString()}", null);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserQueryResponse>))]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new UserQueryRequest());

            return Ok(response);
        }

    }
}
