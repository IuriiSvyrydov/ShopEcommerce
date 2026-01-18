


namespace Catalog.Application.Features.Products.Handlers;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var brand = await _productRepository.GetBrandByIdAsync(request.BrandId);
        var type = await _productRepository.GetTypeByIdAsync(request.TypeId);
        if (brand == null || type == null)
        {
            throw new ApplicationException("Invalid BrandId or TypeId");
        }
        var productEntity = request.ToEntity(brand, type);
        var createdProduct = await _productRepository.CreateProductAsync(productEntity);
        return createdProduct.ToResponse();


    }
}
