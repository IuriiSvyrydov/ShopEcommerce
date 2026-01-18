using Catalog.Application.Features.Products.Queries.GetAll;

namespace Catalog.Application.Features.Products.Handlers;

public sealed class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, IList<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IList<BrandResponse>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandRepository.GetBrandsAsync();
        return brands.ToResponseList();

    }
}
