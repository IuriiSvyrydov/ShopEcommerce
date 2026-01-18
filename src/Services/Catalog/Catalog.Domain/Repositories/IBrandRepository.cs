

namespace Catalog.Domain.Repositories;

 public interface IBrandRepository
{
    Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
    Task<ProductBrand> GetBrandByIdAsync(string id);
}
