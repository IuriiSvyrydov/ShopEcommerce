
using Discount.Application.Helpers;
using Discount.Domain.Repositories;

namespace Discount.Application.Features.Handlers;

public sealed class DeleteDiscountCommandHandler: IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _discountRepository;
    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            var validationError = new Dictionary<string, string>
            {
                { "ProductName", "ProductName cannot be null or empty." }

            };
            throw GrpcErrorHelper.CreateValidationException(validationError);
        }
        var discountToDelete = await _discountRepository.DeleteDiscountAsync(request.ProductName);
        return discountToDelete;
        
    }
}