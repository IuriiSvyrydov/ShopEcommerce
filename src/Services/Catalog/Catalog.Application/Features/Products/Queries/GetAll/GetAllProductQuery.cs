namespace Catalog.Application.Features.Products.Queries.GetAll;

public record GetAllProductQuery(CatalogSpecParams CatalogSpecParams)
    :IRequest<Pagination<ProductResponse>>
{
    public string Name { get; init; }
    public string Summary { get; init; }
    public string Description { get; init; }
    public string ImageFile { get; init; }
    public ProductBrand Brands { get; init; }
    public ProductType Type { get; init; }
    public decimal Price { get; init; }
    public DateTimeOffset CreateDate { get; init; }

}
