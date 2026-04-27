using Catalog.Entities;
using Catalog.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.Repositories.Implementations;

public class TypeRepository : ITypeRepository
{
    private readonly IMongoCollection<ProductType> _types;

    public TypeRepository(IConfiguration configuration)
    {
        var client =
            new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

        var db =
            client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

        _types =
            db.GetCollection<ProductType>(configuration["DatabaseSettings:TypeCollectionName"]);
    }

    public async Task<IEnumerable<ProductType>> GetAllTypesAsync()
    {
        return await _types.Find(_ => true).ToListAsync();
    }

    public async Task<ProductType> GetTypeByIdAsync(string typeId)
    {
        return await _types.Find(t => t.Id == typeId).FirstOrDefaultAsync();
    }
}
