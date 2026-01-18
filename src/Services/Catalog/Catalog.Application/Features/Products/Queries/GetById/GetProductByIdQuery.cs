

namespace Catalog.Application.Features.Products.Queries.GetById
{
    public record class GetProductByIdQuery(string Id) : IRequest<ProductResponse>;
    
       
    

}
