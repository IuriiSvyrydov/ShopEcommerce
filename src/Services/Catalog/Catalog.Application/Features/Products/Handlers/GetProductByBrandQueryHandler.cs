

using Catalog.Application.Features.Products.Queries.GetAll;

namespace Catalog.Application.Features.Products.Handlers;

public sealed class GetProductByBrandQueryHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByBrandQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductByBrand(request.BrandName);
        return (IList<ProductResponse>)products.ToResponseList();
    }
}
