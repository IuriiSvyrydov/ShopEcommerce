


namespace Catalog.Application.Features.Products.Handlers;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)=>
        await _productRepository.DeleteProductAsync(request.ProductId);
        
    
}
