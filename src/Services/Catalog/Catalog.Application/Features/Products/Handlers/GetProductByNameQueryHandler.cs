using Catalog.Application.Features.Products.Queries.GetByName;

namespace Catalog.Application.Features.Products.Handlers;

public sealed class GetProductByNameQueryHandler: IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetProductByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsByNamesAsync(request.productName);
        var productResponses = products.Select(p => p.ToResponse()).ToList();
        return productResponses;
    }
}
