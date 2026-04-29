using Catalog.Entities;
using Catalog.Responses.Product;
using Catalog.Specifications;

namespace Catalog.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToResponse(this Product product)
    {
        if (product is null) return null;

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Summary = product.Summary,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Price = product.Price,
            Brand = product.Brand,
            Type = product.Type,
            CreatedDate = product.CreatedDate
        };
    }

    public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination)
    {
        return new Pagination<ProductResponse>
        {
            PageIndex = pagination.PageIndex,
            PageSize = pagination.PageSize,
            Count = pagination.Count,
            Data = pagination.Data.Select(p => p.ToResponse()).ToList()
        };
    }
}
