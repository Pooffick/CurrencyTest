using System.Security.Claims;
using FinanceService.Application.Users.Commands;
using FinanceService.Application.Users.Dtos;
using FinanceService.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.Api.Controllers
{
    [ApiController]
    [Route("api/currencies")]
    public class CurrencyController(IMediator mediator) : ControllerBase
    {
        [Authorize]
        [HttpGet("my")]
        public async Task<ActionResult<IReadOnlyCollection<CurrencyDto>>> GetMyCurrencies(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await mediator.Send(new GetUserCurrenciesQuery(userId), cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("favorites/{currencyId}")]
        public async Task<IActionResult> AddFavoriteCurrency(string currencyId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            await mediator.Send(new AddFavoriteCurrencyCommand(userId, currencyId), cancellationToken);
            return NoContent();
        }
    }
}
