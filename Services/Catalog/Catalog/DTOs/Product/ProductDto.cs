using Catalog.DTOs.Brand;
using Catalog.DTOs.Type;

namespace Catalog.DTOs.Product;

public record ProductDto
(
    string Id,
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    decimal Price,
    BrandDto Brand,
    TypeDto Type,
    DateTimeOffset CreatedDate
);
