

using Payment.Application.Events;
using Payment.Application.Features.Commands;

namespace Payment.Application.Features.Handlers;

public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Unit>
{
    private readonly IPaymentService _paymentService;
    public ProcessPaymentCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    public async Task<Unit> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        await _paymentService.ProcessPaymentAsync(request.OrdrId,cancellationToken);
        return Unit.Value;
    }
}
