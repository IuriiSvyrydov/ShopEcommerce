


using Catalog.Application.Features.Products.Queries.GetAll;

namespace Catalog.Application.Features.Products.Handlers;

public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Pagination<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsAsync(request.CatalogSpecParams);
        var productResoonse = products.ToPaginationProductResponse();
        return productResoonse;

    }
}
