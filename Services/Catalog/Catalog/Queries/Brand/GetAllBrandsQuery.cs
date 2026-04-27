using Catalog.Responses.Brand;
using MediatR;

namespace Catalog.Queries.Brand;

public record GetAllBrandsQuery : IRequest<IList<BrandResponse>>
{
}
