namespace Infrastructure.Messages.Common;

public class EventBusConstant
{
    public const string BasketCheckoutQueue = "basket-checkout-queue";
    public const string OrderCreatedQueue = "order-created-queue";
    public const string PaymentCompletedQueue = "payment-completed-queue";
    public const string PaymentFailedQueue = "payment-failed-queue";
    
}