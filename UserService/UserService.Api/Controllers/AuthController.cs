using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Models;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Dtos;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterUserRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(
                new RegisterUserCommand(request.Name, request.Password), ct);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(
                new LoginUserCommand(request.Name, request.Password), ct);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            await _mediator.Send(new LogoutUserCommand(), ct);
            return Ok();
        }
    }
}
