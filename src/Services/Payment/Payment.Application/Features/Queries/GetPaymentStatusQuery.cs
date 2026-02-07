
namespace Payment.Application.Features.Queries;

public record GetPaymentStatusQuery(Guid OrderId): IRequest<PaymentStatusDto>;

