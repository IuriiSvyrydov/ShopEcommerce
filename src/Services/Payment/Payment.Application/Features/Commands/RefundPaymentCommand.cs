
namespace Payment.Application.Features.Commands;

public record RefundPaymentCommand(
    Guid PaymentId,
    string Reason) : IRequest<Unit>;

