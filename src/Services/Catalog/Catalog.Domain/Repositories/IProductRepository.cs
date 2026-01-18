namespace Catalog.Domain.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<IReadOnlyList<Product>> GetProductsByNamesAsync(string name);
    Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams);
    Task<IReadOnlyList<Product>> GetProductByBrand(string name);
    Task<ProductBrand> GetBrandByIdAsync(string brandId);
    Task<Product> GetProductByIdAsync(string productId);
    Task<Product>CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(string productId);
   
    Task<ProductType> GetTypeByIdAsync(string id);

}
