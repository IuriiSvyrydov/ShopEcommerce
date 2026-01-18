

namespace Catalog.Application.Mapping;

public static class TypeMapper
{
    public static TypeResponse ToResponse(this ProductType type)
    {
        if (type == null) return null;

        return new TypeResponse
        {
            Id = type.Id,
            Name = type.Name
        };
    }
    public static IList<TypeResponse> ToResponseList(this IReadOnlyList<ProductType> types)
    {
        return types.Select(t => t.ToResponse()).ToList();
    }
}
