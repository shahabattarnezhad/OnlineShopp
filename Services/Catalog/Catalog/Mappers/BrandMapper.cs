using Catalog.Entities;
using Catalog.Responses.Brand;

namespace Catalog.Mappers;

public static class BrandMapper
{
    public static BrandResponse ToResponse(this ProductBrand brand)
    {
        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }

    public static IList<BrandResponse> ToResponseList(this IEnumerable<ProductBrand> brands)
    {
        return brands.Select(brand => brand.ToResponse()).ToList();
    }
}
