using BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var res = await _mediator.Send(command);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }

    [HttpPost("Confirm")]
    public async Task<IActionResult> ConfirmEmail(Guid id)
    {
        var guid = await _mediator.Send(new ConfirmEmailCommand() {ConfirmationId = id});

        return Ok(guid);
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody]ChangeUserPasswordCommand command)
    {
        command.UserId ??= UserId;

        var guid = await _mediator.Send(command);

        return Ok(guid);
    }
}