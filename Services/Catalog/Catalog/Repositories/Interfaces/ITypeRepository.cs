using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface ITypeRepository
{
    Task<IEnumerable<ProductType>> GetAllTypesAsync();

    Task<ProductType> GetTypeByIdAsync(string typeId);
}
