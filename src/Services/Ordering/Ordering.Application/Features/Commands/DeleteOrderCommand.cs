

namespace Ordering.Application.Features.Commands;

public record DeleteOrderCommand:  IRequest<Unit>
{
    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; set; }
}

   

