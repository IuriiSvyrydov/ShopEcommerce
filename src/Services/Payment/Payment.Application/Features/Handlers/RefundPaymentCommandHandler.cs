namespace Payment.Application.Features.Handlers;

public sealed class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand, Unit>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentGateway _paymentGateway;
    private IPublishEndpoint _publishEndpoint;
    public RefundPaymentCommandHandler(IPaymentRepository paymentRepository,
        IPaymentGateway paymentGateway,
        IPublishEndpoint publishEndpoint)
    {
        _paymentRepository = paymentRepository;
        _paymentGateway = paymentGateway;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
        if(payment is null)
            throw new InvalidOperationException($"Payment  not found.");
        if(payment.Status != PaymentStatus.Paid)
            throw new InvalidOperationException($"Only paid payments can be refunded.");
        await _paymentGateway.RefundAsync(payment.ProviderPaymentId!,request.Reason);
        payment.MarkRefunded();
        await _publishEndpoint.Publish(new PaymentRefundedEvent
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Reason = request.Reason,
            CorrelationId = payment.CorrelationId
        },cancellationToken);
        return Unit.Value;


    }
}
