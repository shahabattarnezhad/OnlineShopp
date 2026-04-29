using Catalog.Responses.Product;
using MediatR;

namespace Catalog.Queries.Product;

public record GetAllProductsByNameQuery(string Name) : IRequest<IList<ProductResponse>>
{
}
