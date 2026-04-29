using Catalog.Mappers;
using Catalog.Queries.Brand;
using Catalog.Repositories.Interfaces;
using Catalog.Responses.Brand;
using MediatR;

namespace Catalog.Handlers.Brand;

public class GetAllBrandsHandler(IBrandRepository brandRepository)
                                : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await brandRepository.GetAllBrandsAsync();

        return brandList.ToResponseList();
    }
}
