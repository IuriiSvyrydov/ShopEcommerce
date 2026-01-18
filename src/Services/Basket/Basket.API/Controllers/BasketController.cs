using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Basket.API.Controllers;

[Authorize] // включаем авторизацию
[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ShoppingCartDto>> GetBasket()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var result = await _mediator.Send(new GetBasketByUserNameQuery(userId));
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ShoppingCartDto>> UpdateBasket([FromBody] CreateShoppingCartCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
           ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var securedCommand = CreateShoppingCartCommand.FromUser(userId, command);
        var result = await _mediator.Send(securedCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
           ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        await _mediator.Send(new DeleteBasketByUserNameCommand(userId));
        return NoContent();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckoutDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                  ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        dto = dto with { UserName = userId };
        await _mediator.Send(new BasketCheckoutCommand(dto));
        return Accepted();
    }
}
