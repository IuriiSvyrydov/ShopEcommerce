

namespace Payment.Application.Dtos;

    public sealed class PaymentStatusDto
    {
        public Guid OrderId { get; init; }
        public string Status { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public string Currency { get; init; } = string.Empty;
        public string? TransactionId { get; init; }
       public DateTime UpdateAt { get; init; }



    }

