


using Payment.Application.Interfaces;
using Payment.Application.Mappings;

namespace Payment.Application.Features.Handlers;

public sealed class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
{
    private readonly IPaymentRepository _paymentRepository;
    public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    public async Task<PaymentDto> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByOrderIdAsync(request.PaymentId, cancellationToken);
        if (payment is null)
        {
           return null;
        }
        return payment?.ToDto();

    }
}
