using Catalog.Entities;
using Catalog.Repositories.Interfaces;
using Catalog.Specifications;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Catalog.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;
    private readonly IMongoCollection<ProductBrand> _brands;
    private readonly IMongoCollection<ProductType> _types;

    public ProductRepository(IConfiguration configuration)
    {
        var client =
            new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

        var db =
            client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

        _products =
            db.GetCollection<Product>(configuration["DatabaseSettings:ProductCollectionName"]);
        _brands =
            db.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandCollectionName"]);
        _types =
            db.GetCollection<ProductType>(configuration["DatabaseSettings:TypeCollectionName"]);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> DeleteProductAsync(string productId)
    {
        var deletedProduct = await _products.DeleteOneAsync(p => p.Id == productId);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
    }

    public async Task<Pagination<Product>> GetAllPaginatedProductsAsync(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(catalogSpecParams.Search))
        {
            filter &= builder
                .Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
        {
            filter &= builder.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);
        }

        if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
        {
            filter &= builder.Eq(p => p.Type.Id, catalogSpecParams.TypeId);
        }

        var totalItems = await _products.CountDocumentsAsync(filter);
        var data = await ApplyDataFilters(catalogSpecParams, filter);

        return new Pagination<Product>(
            catalogSpecParams.PageIndex,
            catalogSpecParams.PageSize,
            (int)totalItems,
            data
        );
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsByBrandAsync(string brandName)
    {
        return await _products.Find(p => p.Brand.Name.ToLower() == brandName.ToLower())
                              .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsByNameAsync(string productName)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, new BsonRegularExpression($".*{productName}.*", "i"));
        return await _products.Find(filter).ToListAsync();
    }

    public async Task<ProductBrand> GetBrandByIdAsync(string brandId)
    {
        return await _brands.Find(b => b.Id == brandId).FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByIdAsync(string productId)
    {
        return await _products.Find(p => p.Id == productId).FirstOrDefaultAsync();
    }

    public async Task<ProductType> GetTypeByIdAsync(string typeId)
    {
        return await _types.Find(t => t.Id == typeId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updatedProduct =
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);

        return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
    }

    private async Task<IReadOnlyCollection<Product>> ApplyDataFilters
        (CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        var sortDefinition = Builders<Product>.Sort.Ascending("Name");

        if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
        {
            sortDefinition = catalogSpecParams.Sort switch
            {
                "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                _ => Builders<Product>.Sort.Ascending(p => p.Name)
            };
        }

        return await _products
                     .Find(filter)
                     .Sort(sortDefinition)
                     .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                     .Limit(catalogSpecParams.PageSize)
                     .ToListAsync();
    }
}
