using Catalog.Responses.Product;
using MediatR;

namespace Catalog.Queries.Product;

public record GetAllProductsByBrandQuery(string BrandName) : IRequest<IList<ProductResponse>>
{
}
