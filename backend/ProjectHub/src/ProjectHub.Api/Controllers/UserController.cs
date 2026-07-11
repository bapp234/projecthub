using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Application.Features.Users.Commands.CreateUser;
using ProjectHub.Application.Features.Users.Queries.GetUserById;

namespace ProjectHub.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public sealed class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            return result is null
                ? NotFound()
                : Ok(result);
        }
    }
}
