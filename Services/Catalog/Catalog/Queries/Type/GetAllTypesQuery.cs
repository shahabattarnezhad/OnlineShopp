using Catalog.Responses.Type;
using MediatR;

namespace Catalog.Queries.Type;

public record GetAllTypesQuery : IRequest<IList<TypesResponse>>
{
}
