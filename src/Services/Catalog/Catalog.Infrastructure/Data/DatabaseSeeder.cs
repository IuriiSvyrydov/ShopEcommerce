

using System.Text.Json;

namespace Infrastructure.Data;

public class DatabaseSeeder
{
    public static async Task SeedAsync(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

        var productsCollection = database.GetCollection<Product>(configuration["DatabaseSettings:ProductCollectionName"]);
        var brandsCollection = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandCollectionName"]);
        var typesCollection = database.GetCollection<ProductType>(configuration["DatabaseSettings:TypeCollectionName"]);

        var seedDataPath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData");

        // --- Seed Brands ---
        List<ProductBrand> brandList = new();
        if (await brandsCollection.CountDocumentsAsync(_ => true) == 0)
        {
            var brandData = await File.ReadAllTextAsync(Path.Combine(seedDataPath, "brands.js"));
            brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            await brandsCollection.InsertManyAsync(brandList);
        }
        else
        {
            brandList = await brandsCollection.Find(_ => true).ToListAsync();
        }

        // --- Seed Types ---
        List<ProductType> typeList = new();
        if (await typesCollection.CountDocumentsAsync(_ => true) == 0)
        {
            var typeData = await File.ReadAllTextAsync(Path.Combine(seedDataPath, "types.js"));
            typeList = JsonSerializer.Deserialize<List<ProductType>>(typeData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            await typesCollection.InsertManyAsync(typeList);
        }
        else
        {
            typeList = await typesCollection.Find(_ => true).ToListAsync();
        }

        // --- Seed Products ---
        if (await productsCollection.CountDocumentsAsync(_ => true) == 0)
        {
            var productData = await File.ReadAllTextAsync(Path.Combine(seedDataPath, "products.js"));
            var productList = JsonSerializer.Deserialize<List<Product>>(productData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            foreach (var product in productList)
            {
                product.Id = null;
                if (product.CreateDate == default)
                    product.CreateDate = DateTime.Now;

                // Прив'язка Brand по назві
                if (!string.IsNullOrEmpty(product.Brands.Name))
                    product.Brands = brandList.FirstOrDefault(b => b.Name == product.Brands.Name);

                // Прив'язка Type по назві
                if (!string.IsNullOrEmpty(product.Type.Name))
                    product.Type = typeList.FirstOrDefault(t => t.Name == product.Type.Name);
            }

            await productsCollection.InsertManyAsync(productList);
        }
    }
}
