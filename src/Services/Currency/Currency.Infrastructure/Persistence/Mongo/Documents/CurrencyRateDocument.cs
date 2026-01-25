

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Currency.Infrastructure.Persistence.Mongo.Documents;


public class CurrencyRateDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string BaseCurrency { get; set; } = default!;
    public string TargetCurrency { get; set; } = default!;
    public decimal Rate { get; set; }
    public DateTime ValidUntil { get; set; }
}
