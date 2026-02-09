
using Payment.Application.Mappings;

namespace Payment.Application.Features.Handlers;

public sealed class GetPaymentQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
{
  
    private readonly IPaymentRepository _paymentRepository;
    public GetPaymentQueryHandler( IPaymentRepository paymentRepository)
    {
        
        _paymentRepository = paymentRepository;
    }
    public async Task<PaymentDto> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(request.PaymentId,cancellationToken);
        if (payment == null)
        {
            throw new InvalidOperationException($"Payment with ID {request.PaymentId} not found.");
        }
        return payment.ToDto();
    }
}
