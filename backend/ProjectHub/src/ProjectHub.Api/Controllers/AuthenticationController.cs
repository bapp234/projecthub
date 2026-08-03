using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Application.Features.Authentication.Commands.Register;
using ProjectHub.Application.Features.Authentication.Commands.Login;
using ProjectHub.Application.Features.Authentication.Commands.Refresh;
namespace ProjectHub.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterCommand), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
    [FromBody] LoginCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(RefreshResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh(
    [FromBody] RefreshCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}