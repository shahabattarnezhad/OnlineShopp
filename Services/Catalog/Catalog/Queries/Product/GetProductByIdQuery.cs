using Catalog.Responses.Product;
using MediatR;

namespace Catalog.Queries.Product;

public record GetProductByIdQuery(string Id) : IRequest<ProductResponse>
{
}
