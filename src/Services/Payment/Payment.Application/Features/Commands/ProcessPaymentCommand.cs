

namespace Payment.Application.Features.Commands
{
    public record ProcessPaymentCommand(Guid OrdrId):IRequest<Unit>;
   
}
