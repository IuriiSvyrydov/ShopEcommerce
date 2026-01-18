

namespace Application.Features.Products.Commands;

public record DeleteProductCommand(string ProductId): IRequest<bool>;

