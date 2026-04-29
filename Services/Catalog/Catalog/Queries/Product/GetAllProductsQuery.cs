using Catalog.Responses.Product;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Queries.Product;

public record GetAllProductsQuery(CatalogSpecParams CatalogSpecParams) 
              : IRequest<Pagination<ProductResponse>>
{
}
