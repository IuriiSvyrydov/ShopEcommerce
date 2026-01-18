

namespace Catalog.Application.Responses;

public record  ProductResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public ProductBrand Brands { get; set; }
    public ProductType Type { get; set; }
  
    public decimal Price { get; set; }
    public DateTimeOffset CreateDate { get; set; }



}