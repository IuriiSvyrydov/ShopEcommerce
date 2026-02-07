namespace Ordering.Application.Features.Commands;

public record CheckoutOrderCommand : IRequest<Guid>
{
    // Данные пользователя/заказа
    public string? UserName { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    // Для платежа оставляем только безопасное
    public string CardName { get; set; }             // Имя владельца карты
    public int PaymentMethod { get; set; }           // Тип метода оплаты (Stripe, PayPal и т.д.)
    public string Currency { get; set; } = "UAH";    // Валюта платежа

    // Корреляция для Outbox и событий
    public Guid CorrelationId { get; set; }
}