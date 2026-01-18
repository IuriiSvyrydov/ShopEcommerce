

namespace Catalog.Application.Mapping;

public static class ProductMapper
{
    public static ProductResponse ToResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Summary = product.Summary,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Price = product.Price,
            Brands = product.Brands,
            Type = product.Type,
            CreateDate = product.CreateDate
        };
    }

    public static IEnumerable<ProductResponse> ToResponseList(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToResponse()).ToList();
    }

    public static Pagination<ProductResponse> ToPaginationProductResponse(this Pagination<Product> products)
    {
        var productResponses = products.Data.Select(p => p.ToResponse()).ToList();
        return new Pagination<ProductResponse>(products.PageIndex, products.PageSize, products.Count, productResponses);
    }

    public static Product ToUpdateProduct(this UpdateProductCommand command, Product product, ProductBrand brand, ProductType type)
        => new Product
        {
            Id = product.Id,
            Name = product.Name,
            Summary = product.Summary,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Brands = brand,
            Type = type,
            Price = product.Price,
            CreateDate = product.CreateDate
        };

    public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type)
        => new Product
        {
            Name = command.Name,
            Summary = command.Summary,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Brands = brand,
            Type = type,
            Price = command.Price,
            CreateDate = DateTimeOffset.UtcNow
        };

    public static ProductDto ToDto(this ProductResponse product)
    {
        if (product == null) return null;

        BrandDto brandDto = null;
        if (product.Brands != null)
        {
            brandDto = new BrandDto(
                product.Brands.Id ?? string.Empty,
                product.Brands.Name ?? string.Empty
            );
        }

        TypeDto typeDto = null;
        if (product.Type != null)
        {
            typeDto = new TypeDto(
                product.Type.Id ?? string.Empty,
                product.Type.Name ?? string.Empty
            );
        }

        return new ProductDto(
            product.Id,
            product.Name,
            product.Summary,
            product.Description,
            product.ImageFile,
            brandDto,
            typeDto,
            product.Price,
            product.CreateDate
        );
    }

    public static UpdateProductCommand ToCommand(this UpdateProductCommand command, string id)
    {
        return new UpdateProductCommand
        {
            Id = id,
            Name = command.Name,
            Summary = command.Summary,
            Description = command.Description,
            ImageFile = command.ImageFile,
            BrandId = command.BrandId,
            TypeId = command.TypeId,
            Price = command.Price
        };
    }
}
