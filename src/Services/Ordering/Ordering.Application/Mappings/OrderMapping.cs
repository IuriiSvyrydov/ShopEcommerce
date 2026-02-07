using Newtonsoft.Json;

namespace Ordering.Application.Mappings;

public static class OrderMapping
{
    /* ===================== ENTITY → DTO ===================== */

    public static OrderDTO ToDto(this Order order)
        => new(
            order.Id,
            order.UserName,
            order.FirstName,
            order.LastName,
            order.TotalPrice,
            order.EmailAddress,
            order.AddressLine,
            order.Country,
            order.State,
            order.ZipCode,
            order.PaymentMethod,
            order.Currency,
            order.OrderStatus
            
        );

    /* ===================== COMMAND → ENTITY ===================== */

    public static Order ToEntity(this CheckoutOrderCommand command)
        => new()
        {
            UserName = command.UserName,
            TotalPrice = command.TotalPrice,
            FirstName = command.FirstName,
            LastName = command.LastName,
            EmailAddress = command.EmailAddress,
            AddressLine = command.AddressLine,
            Country = command.Country,
            State = command.State,
            ZipCode = command.ZipCode,
            PaymentMethod = command.PaymentMethod,
            Currency = command.Currency,
            OrderStatus = OrderStatus.Pending
        };

    /* ===================== UPDATE ===================== */

    public static void MapUpdateOrder(this Order order, UpdateOrderCommand command)
    {
        order.UserName = command.UserName;
        order.TotalPrice = command.TotalPrice;
        order.FirstName = command.FirstName;
        order.LastName = command.LastName;
        order.EmailAddress = command.EmailAddress;
        order.AddressLine = command.AddressLine;
        order.Country = command.Country;
        order.State = command.State;
        order.ZipCode = command.ZipCode;
        order.PaymentMethod = command.PaymentMethod;
    }

    /* ===================== DTO → COMMAND ===================== */

    public static CheckoutOrderCommand ToCommand(this CreateOrderDto dto)
        => new()
        {
            UserName = dto.UserName,
            TotalPrice = dto.TotalPrice,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            AddressLine = dto.AddressLine,
            Country = dto.Country,
            State = dto.State,
            ZipCode = dto.ZipCode,
            CardName = dto.CardName,
            PaymentMethod = dto.PaymentMethod,
            Currency = dto.Currency
        };

    public static UpdateOrderCommand ToCommand(this OrderDTO dto)
        => new()
        {
            Id = dto.Id,
            UserName = dto.UserName,
            TotalPrice = dto.TotalPrice,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            AddressLine = dto.AddressLine,
            Country = dto.Country,
            State = dto.State,
            ZipCode = dto.ZipCode,
         
            PaymentMethod = dto.PaymentMethod
        };

    /* ===================== BASKET EVENT ===================== */
    public static CheckoutOrderCommand ToCheckoutOrderCommand(this BasketCheckoutEvent message)
        => new()
        {
            UserName = message.UserId,          // ← UserId используем как UserName
            TotalPrice = message.TotalPrice,
            FirstName = message.FirstName,
            LastName = message.LastName,
            EmailAddress = message.EmailAddress,
            AddressLine = message.AddressLine,
            Country = message.Country,
            State = message.State,
            ZipCode = message.ZipCode,
            PaymentMethod = message.PaymentMethod,
            Currency = message.Currency,
            CorrelationId = message.CorrelationId
        };

   

    /* ===================== OUTBOX ===================== */

    public static OutboxMessage ToOrderCreatedOutboxMessage(Order order, Guid correlationId)
        => new()
        {
            CorrelationId = correlationId.ToString(),
            Type = OutboxMessageTypes.OrderCreated,
            OccurredOn = DateTime.UtcNow,
            Content = JsonConvert.SerializeObject(new
            {
                OrderId = order.Id,
                order.UserName,
                order.TotalPrice,
                order.Currency,
                order.PaymentMethod,
                order.OrderStatus
            })
        };

    public static OutboxMessage ToOrderUpdatedOutboxMessage(Order order, Guid correlationId)
        => new()
        {
            CorrelationId = correlationId.ToString(),
            Type = OutboxMessageTypes.OrderCreated,
            OccurredOn = DateTime.UtcNow,
            Content = JsonConvert.SerializeObject(new
            {
                OrderId = order.Id,
                order.OrderStatus
            })
        };
}
