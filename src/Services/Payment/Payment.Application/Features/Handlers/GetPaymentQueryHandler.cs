


using Payment.Application.Events;
using Payment.Application.Interfaces;

namespace Payment.Application.Features.Handlers;

public sealed class GetPaymentQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
{
    private readonly IPaymentService _paymentService;
    private readonly IPaymentRepository _paymentRepository;
    public GetPaymentQueryHandler(IPaymentService paymentService, IPaymentRepository paymentRepository)
    {
        _paymentService = paymentService;
        _paymentRepository = paymentRepository;
    }
    public Task<PaymentDto> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
