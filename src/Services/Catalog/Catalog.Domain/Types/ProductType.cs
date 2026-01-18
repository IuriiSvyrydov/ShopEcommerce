

namespace Catalog.Domain.Types;

public class ProductType: BaseEntity
{
    [BsonElement("name")]
    public string Name { get; set; }
}
