namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    #region private fields and constructors

    private readonly IMongoCollection<Product> _products;
    private readonly IMongoCollection<ProductBrand> _brands;
    private readonly IMongoCollection<ProductType> _types;
    
    public ProductRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        _products = database.GetCollection<Product>(configuration["DatabaseSettings:ProductCollectionName"]);
        _brands = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandCollectionName"]);
        _types = database.GetCollection<ProductType>(configuration["DatabaseSettings:TypeCollectionName"]);
        
    }
    #endregion

    #region Methods
    

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _products.Find(p => true).ToListAsync();
    }

    public async Task<IReadOnlyList<Product>> GetProductsByNamesAsync(string name)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, new BsonRegularExpression($".*{name}.*", "i"));
        return await _products.Find(filter).ToListAsync();
        
    }

    public async Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams)
    {
        var buillder = Builders<Product>.Filter;
        var filter = buillder.Empty;
        if (!string.IsNullOrEmpty(specParams.Search))
        {
            filter &= buillder.Where(p => p.Name.ToLower().Contains(specParams.Search.ToLower())); 
        }
        if (!string.IsNullOrEmpty(specParams.BrandId))
        {
            // защита от null: убедиться, что Brands не null перед доступом к Id
            filter &= buillder.Where(p => p.Brands != null && p.Brands.Id == specParams.BrandId);
        }
        if (!string.IsNullOrEmpty(specParams.TypeId))
        {
            // защита от null: убедиться, что Type не null перед доступом к Id
            filter &= buillder.Where(p => p.Type != null && p.Type.Id == specParams.TypeId);
        }
        var totalItems = await _products.CountDocumentsAsync(filter);
        var data = await ApplyDaataFilter(specParams, filter);
        return new Pagination<Product>(specParams.PageIndex,
            specParams.PageSize,(int)totalItems,
            data);
    }

    private async Task<IReadOnlyList<Product>> ApplyDaataFilter(CatalogSpecParams specParams, FilterDefinition<Product> filter)
    {
        var sortDefn = Builders<Product>.Sort.Ascending("Name");
        if (!string.IsNullOrEmpty(specParams.Sort))
        {
            switch (specParams.Sort)
            {
                case "priceAsc":
                    sortDefn = Builders<Product>.Sort.Ascending("Price");
                    break;
                case "priceDesc":
                    sortDefn = Builders<Product>.Sort.Descending("Price");
                    break;
                default:
                    sortDefn = Builders<Product>.Sort.Ascending("Name");
                    break;
            }
        }
        return await _products.Find(filter)
            .Sort(sortDefn)
            .Skip(specParams.PageSize * (specParams.PageIndex - 1))
            .Limit(specParams.PageSize)
            .ToListAsync();

    }

    public async Task<IReadOnlyList<Product>> GetProductByBrand(string name)
    {
        if (string.IsNullOrEmpty(name))
            return new List<Product>();

        // защита от null: убедиться, что Brands не null перед доступом к Name
        return await _products.Find(p => p.Brands != null && p.Brands.Name.ToLower() == name.ToLower()).ToListAsync();
    }

    public async Task<ProductBrand> GetBrandByIdAsync(string brandId)
    {
        return await _brands.Find(p => p.Id == brandId).FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByIdAsync(string productId)
    {
        return await _products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updateProduct = await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string productId)
    {
        var deleteProduct = await _products.DeleteOneAsync(p => p.Id == productId);
        return deleteProduct.IsAcknowledged && deleteProduct.DeletedCount > 0;
    }
    
    public async Task<ProductType> GetTypeByIdAsync(string id)
    
    {
        return await _types.Find(p => p.Id == id).FirstOrDefaultAsync();
    }        

    #endregion
}