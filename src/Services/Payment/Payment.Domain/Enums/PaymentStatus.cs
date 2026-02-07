
namespace Payment.Domain.Enums;

public enum PaymentStatus
{
    Pending = 0,          // создан, ждёт оплаты
    Processing = 1,       // запрос ушёл в платёжный провайдер
    Paid = 2,             // деньги подтверждены
    Failed = 3,           // платёж отклонён
    Refunded = 4          // возврат выполнен

}
