



namespace Catalog.Application.Features.Products.Handlers;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = await _productRepository.GetProductByIdAsync(request.Id);
        if (productToUpdate is null)
        {
            throw new KeyNotFoundException($"Product with Id: {request.Id} was not found.");
        }
        var brand = await _productRepository.GetBrandByIdAsync(request.BrandId);
        var type = await _productRepository.GetTypeByIdAsync(request.TypeId);
        if (brand is null || type is null)
        {
            throw new ApplicationException("Invalid BrandId or TypeId.");

        }
        var updateProduct = request.ToUpdateProduct(productToUpdate, brand, type);
        return await _productRepository.UpdateProductAsync(updateProduct);

    }
}
