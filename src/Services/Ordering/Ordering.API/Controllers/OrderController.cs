
namespace Ordering.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private IMediator _mediator;
    private ILogger<OrderController> _logger;
    public OrderController(IMediator mediator, ILogger<OrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet("{userName}", Name = "GetOrdersByUserName")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUserName([FromRoute]string userName)
    {
        _logger.LogInformation("Getting orders for user: {userName}", userName);
        var query = new GetOrderListQuery(userName);
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }
    [HttpPost(Name = "CheckoutOrder")]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CreateOrderDto dto)
    {
        
        // Extract CorrelationId from request headers
        var correlationId = HttpContext.Request.Headers["x-correlation-id"]
            .FirstOrDefault()??Guid.NewGuid().ToString();
      var command = dto.ToCommand();
      command.CorrelationId = Guid.Parse(correlationId);
        var result = await _mediator.Send(command);
        _logger.LogInformation($"Order created with id: {result},CorrelationId: {correlationId}");
        return Ok(result);

    }
    [HttpPut(Name = "UpdateOrder")]
    public async Task<ActionResult> UpdateOrder([FromBody] OrderDTO dto)
    {
        var correlationId = HttpContext.Request.Headers["x-correlation-id"]
            .FirstOrDefault()??Guid.NewGuid().ToString();
        var command = dto.ToCommand();
        command.CorrelationId = Guid.Parse(correlationId);
        await _mediator.Send(command);
        _logger.LogInformation($"Order updated with id:{dto.Id},CorrelationId: {correlationId}");
        return NoContent();
    }
    [HttpDelete("{id}", Name = "DeleteOrder")]
    public async Task<ActionResult> DeleteOrder([FromRoute]int id)
    {
        var correlationId = HttpContext.Request.Headers["x-correlation-id"]
            .FirstOrDefault()??Guid.NewGuid().ToString();
        var command = new DeleteOrderCommand
        {
            OrderId = id,
            CorrelationId = Guid.Parse(correlationId)
            
        };
        command.CorrelationId = Guid.Parse(correlationId);
        await _mediator.Send(command);
        _logger.LogInformation($"Order deleted with id:{id},CorrelationId: {correlationId}");
        return NoContent();
    }

}
