namespace Catalog.Application.Features.Products.Queries.GetAll
{
    public record class GetAllBrandQuery : IRequest<IList<BrandResponse>>;
    
}
