


namespace Catalog.Application.Mapping;

public static class BrandMapper
{
    public static BrandResponse ToResponse(this ProductBrand brand)
    {
        if (brand == null) return null;
        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }

    public static IList<BrandResponse> ToResponseList(this IReadOnlyList<ProductBrand> brands)
    {
        return brands?.Select(b => b.ToResponse()).ToList() ?? new List<BrandResponse>();
    }
}