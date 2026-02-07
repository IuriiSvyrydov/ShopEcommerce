namespace Payment.Application.Features.Queries;

    public record  GetPaymentByIdQuery(Guid PaymentId) : IRequest<PaymentDto>;

