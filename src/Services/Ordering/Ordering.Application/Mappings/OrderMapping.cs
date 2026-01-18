

using Newtonsoft.Json;

namespace Ordering.Application.Mappings;

public static class OrderMapping
{
    public static OrderDTO ToDtos(this Order orders)
    => new OrderDTO(
        orders.Id,
        orders.UserName,
        orders.TotalPrice,
        orders.FirstName,
        orders.LastName,
        orders.EmailAddress,
        orders.AddressLine,
        orders.Country,
        orders.State,
        orders.ZipCode,
        orders.CardName,
        orders.CardNumber,
        orders.Expiration,
        orders.CVV,
        orders.PaymentMethod
        );
    public static Order ToEntity(this CheckoutOrderCommand command)
        => new Order
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
            CardName = command.CardName,
            CardNumber = command.CardNumber,
            Expiration = command.Expiration,
            CVV = command.CVV,
            PaymentMethod = command.PaymentMethod
        };
    public static void MapUpdateOrder(this Order order, UpdateOrderCommand command)
        => new Order
        {
            Id = command.Id,
            UserName = command.UserName,
            TotalPrice = command.TotalPrice,
            FirstName = command.FirstName,
            LastName = command.LastName,
            EmailAddress = command.EmailAddress,
            AddressLine = command.AddressLine,
            Country = command.Country,
            State = command.State,
            ZipCode = command.ZipCode,
            CardName = command.CardName,
            CardNumber = command.CardNumber,
            Expiration = command.Expiration,
            CVV = command.CVV,
            PaymentMethod = command.PaymentMethod
        };
    public static CheckoutOrderCommand ToCommand(this CreateOrderDto dto)
        => new CheckoutOrderCommand
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
            CardNumber = dto.CardNumber,
            Expiration = dto.Expiration,
            CVV = dto.CVV,
            PaymentMethod = dto.PaymentMethod
        };
    public static UpdateOrderCommand ToCommand(this OrderDTO dto)
        => new UpdateOrderCommand
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
            CardName = dto.CardName,
            CardNumber = dto.CardNumber,
            Expiration = dto.Expiration,
            CVV = dto.CVV,
            PaymentMethod = dto.PaymentMethod,
            
        };
    public static CheckoutOrderCommand ToCheckoutOrderCommand(this BasketCheckoutEvent message)
    {
        return new()
        {
            UserName = message.UserName,
            TotalPrice = (decimal)message.TotalPrice,
            FirstName = message.FirstName,
            LastName = message.LastName,
            EmailAddress = message.EmailAddress,
            AddressLine = message.AddressLine,
            Country = message.Country,
            State = message.State,
            ZipCode = message.ZipCode,
            CardName = message.CardName,
            CardNumber = message.CardNumber,
            Expiration = message.Expiration,
            CVV = message.Cvv,
            PaymentMethod = int.TryParse(message.PaymentMethod, out var method) ? method : 0
        };
       
    }
    public static OutboxMessage ToOutboxMessage(Order order, Guid correlationId)
    {
        return new OutboxMessage
        {
            CorrelationId = correlationId.ToString(),
            Type = OutboxMessageTypes.OrderCreated,
            OccurredOn = DateTime.UtcNow,
            Content = JsonConvert.SerializeObject(new
            {
                order.Id,
                order.UserName,
                order.TotalPrice,
                order.FirstName,
                order.LastName,
                order.EmailAddress,
                order.AddressLine,
                order.Country,
                order.State,
                order.ZipCode,
                order.CardName,
                order.CardNumber,
                order.Expiration,
                order.CVV,
                order.PaymentMethod,
                order.OrderStatus
            })
        };       
        
    }

    public static OutboxMessage ToOutboxMessageForUpdate(Order order, Guid correlationId)
    {
        return new OutboxMessage
        {
            CorrelationId = correlationId.ToString(),
            Type = OutboxMessageTypes.OrderCreated,
            OccurredOn = DateTime.UtcNow,
            Content = JsonConvert.SerializeObject(new
                {
                    order.Id,
                    order.UserName,
                    order.TotalPrice,
                    order.FirstName,
                    order.LastName,
                    order.EmailAddress,
                    order.AddressLine,
                    order.Country,
                    order.State,
                    order.ZipCode,
                    order.CardName,
                    order.CardNumber,
                    order.Expiration,
                    order.CVV,
                    order.PaymentMethod,
                    order.OrderStatus

                })
        };
    }
}
