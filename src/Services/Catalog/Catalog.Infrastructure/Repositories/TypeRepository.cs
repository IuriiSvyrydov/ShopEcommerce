

namespace Catalog.Infrastructure.Repositories;

    public class TypeRepository : ITypeRepository
    {
        private readonly IMongoCollection<ProductType> _types;
        public TypeRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            _types = database.GetCollection<ProductType>(configuration["DatabaseSettings:TypeCollectionName"]);
        }
        public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
        {
            return await _types.Find(p => true).ToListAsync();
        }

        public async Task<ProductType> GetTypeByIdAsync(string id)
        {
            return await _types.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
