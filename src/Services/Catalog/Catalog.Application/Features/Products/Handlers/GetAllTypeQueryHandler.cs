

using Catalog.Application.Features.Products.Queries.GetAll;

namespace Catalog.Application.Features.Products.Handlers;

public sealed class GetAllTypeQueryHandler : IRequestHandler<GetAllTypeQuery, IList<TypeResponse>>
{
    private readonly ITypeRepository _typeRepository;

    public GetAllTypeQueryHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<IList<TypeResponse>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
    {
       var types = await _typeRepository.GetTypesAsync();
        return types.ToResponseList();


    }
}
