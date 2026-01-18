

namespace Catalog.Domain.Brands;

public class ProductBrand : BaseEntity
{
    [BsonElement("name")]
    public string Name { get; set; }

  
}
