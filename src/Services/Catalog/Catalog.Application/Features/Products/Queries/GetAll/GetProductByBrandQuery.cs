
namespace Catalog.Application.Features.Products.Queries.GetAll;

public record GetProductByBrandQuery(string BrandName) : IRequest<List<ProductResponse>>;


