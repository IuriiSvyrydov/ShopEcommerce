
namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{paymentId:guid}")]
        public async Task<IActionResult> GetById(Guid paymentId)
        {
            var result = await _mediator.Send(new GetPaymentByIdQuery(paymentId));
            return Ok(result);
        }
        [HttpGet("order/{orderId:guid}")]
        public async Task<IActionResult> GetByOrderId(Guid orderId)
        {
            var result = await _mediator.Send(new GetPaymentStatusQuery(orderId));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
             await _mediator.Send(command);
            
            return Accepted();
        }
        [HttpPost("refund")]
        public async Task<IActionResult> RefundPayment([FromBody] RefundPaymentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
