
using Payment.Application.Interfaces;
using Payment.Application.Mappings;

namespace Payment.Application.Features.Handlers;

public sealed class GetPaymentStatusQueryHandler : IRequestHandler<GetPaymentStatusQuery, PaymentStatusDto>
{
    private readonly IPaymentRepository _paymentRepository;
    public GetPaymentStatusQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    public async Task<PaymentStatusDto> Handle(GetPaymentStatusQuery request, CancellationToken cancellationToken)
    {
        var paymentStatus = await _paymentRepository.GetByOrderIdAsync(request.OrderId, cancellationToken);
        return paymentStatus?.ToStatusDto();
    }
}
