namespace Payment.Application.Contracts;

public class PaymentResult
{
    public bool Success { get; init; }
    public string? TransactionId { get; init; }
    public string? ErrorMessage { get; init; }

    public PaymentResult()
    {
        
    }

    public static PaymentResult Ok(string transactionId) =>
        new()
        {
            Success = true,
            TransactionId = transactionId
        };
    public static PaymentResult Fail(string errorMessage) =>
        new()
        {
            Success = false,
            ErrorMessage = errorMessage
        };
}