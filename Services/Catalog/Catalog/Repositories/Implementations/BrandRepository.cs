using Catalog.Entities;
using Catalog.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.Repositories.Implementations;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> _brands;

    public BrandRepository(IConfiguration configuration)
    {
        var client =
            new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

        var db =
            client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

        _brands =
            db.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandCollectionName"]);
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
    {
        return await _brands.Find(_ => true).ToListAsync();
    }

    public async Task<ProductBrand> GetBrandByIdAsync(string brandId)
    {
        return await _brands.Find(b => b.Id == brandId).FirstOrDefaultAsync();
    }
}
