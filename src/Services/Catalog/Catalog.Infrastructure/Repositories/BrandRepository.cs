using Catalog.Domain.Brands;
using Catalog.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> _brands;
    public BrandRepository(IConfiguration configuration)
    {
        var client = new  MongoClient(configuration["DatabaseSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
         _brands = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandCollectionName"]);

    }
    public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
    {
        return await _brands.Find(p => true).ToListAsync();
    }

    public async Task<ProductBrand> GetBrandByIdAsync(string id)
    {
        return await _brands.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
}