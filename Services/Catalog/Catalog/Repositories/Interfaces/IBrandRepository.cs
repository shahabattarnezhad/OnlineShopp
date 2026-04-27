using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrandsAsync();

    Task<ProductBrand> GetBrandByIdAsync(string brandId);
}
