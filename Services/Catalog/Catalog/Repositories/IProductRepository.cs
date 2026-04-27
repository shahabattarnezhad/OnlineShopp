using Catalog.Entities;
using Catalog.Specifications;

namespace Catalog.Repositories;

public interface IProductRepository
{
    Task<Pagination<Product>> GetAllPaginatedProductsAsync(CatalogSpecParams specParams);

    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<IEnumerable<Product>> GetAllProductsByNameAsync(string productName);

    Task<IEnumerable<Product>> GetAllProductsByBrandAsync(string brandName);

    Task<Product> GetProductByIdAsync(string productId);

    Task<Product> CreateProductAsync(Product product);

    Task<bool> UpdateProductAsync(Product product);

    Task<bool> DeleteProductAsync(string productId);

    Task<ProductBrand> GetBrandByBrandIdAsync(string brandId);

    Task<ProductBrand> GetTypeByTypeIdAsync(string typeId);
}
