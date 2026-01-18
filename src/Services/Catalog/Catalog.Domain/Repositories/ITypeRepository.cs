

namespace Catalog.Domain.Repositories;

public interface ITypeRepository
{
    Task<IReadOnlyList<ProductType>> GetTypesAsync();
    Task<ProductType> GetTypeByIdAsync(string id);
}
