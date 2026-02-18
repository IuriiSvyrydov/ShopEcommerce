using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Basket.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string GetUserId()
    {
        var userId =
         User.FindFirstValue(ClaimTypes.NameIdentifier) ??
         User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrWhiteSpace(userId))
            throw new UnauthorizedAccessException("UserId claim not found.");

        return userId;
    }

    [HttpGet]
    public async Task<ActionResult<ShoppingCartDto>> GetBasket()
    {
        var userId = GetUserId();

        var result = await _mediator.Send(
            new GetBasketByUserNameQuery(userId));

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ShoppingCartDto>> UpdateBasket(
        [FromBody] CreateShoppingCartCommand command)
    {
        var userId = GetUserId();

        var securedCommand = CreateShoppingCartCommand
            .FromUser(userId, command);

        var result = await _mediator.Send(securedCommand);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        var userId = GetUserId();

        await _mediator.Send(
            new DeleteBasketByUserNameCommand(userId));

        return NoContent();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(
        [FromBody] BasketCheckoutDto dto)
    {
        var userId = GetUserId();

        var securedDto = dto with { UserName = userId };

        await _mediator.Send(
            new BasketCheckoutCommand(securedDto));

        return Accepted();
    }
}
