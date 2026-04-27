using Catalog.Extensions;
using Catalog.Queries.Type;
using Catalog.Repositories.Interfaces;
using Catalog.Responses.Type;
using MediatR;

namespace Catalog.Handlers.Type;

public class GetAllTypesHandler(ITypeRepository type)
                               : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
{
    public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typeList = await type.GetAllTypesAsync();
        return typeList.ToResponseList();
    }
}
