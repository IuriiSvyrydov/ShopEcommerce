

namespace Ordering.Application.Features.Commands;

public record DeleteOrderCommand:  IRequest<Unit>
{
    public int OrderId { get; set; }
    public Guid CorrelationId { get; set; }
}

   

