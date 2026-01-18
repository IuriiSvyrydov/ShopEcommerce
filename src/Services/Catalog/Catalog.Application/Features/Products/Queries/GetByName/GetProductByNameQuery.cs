

namespace Catalog.Application.Features.Products.Queries.GetByName;

public record GetProductByNameQuery(string productName) : IRequest<IList<ProductResponse>>;

