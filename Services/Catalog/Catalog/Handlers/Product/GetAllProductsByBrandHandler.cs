using Catalog.Mappers;
using Catalog.Queries.Product;
using Catalog.Repositories.Interfaces;
using Catalog.Responses.Product;
using MediatR;

namespace Catalog.Handlers.Product;

public class GetAllProductsByBrandHandler(IProductRepository productRepository)
             : IRequestHandler<GetAllProductsByBrandQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetAllProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = 
            await productRepository.GetAllProductsByBrandAsync(request.BrandName);

        return productList.ToResponseList().ToList();
    }
}
