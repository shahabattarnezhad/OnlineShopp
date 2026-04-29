using Catalog.Mappers;
using Catalog.Queries.Product;
using Catalog.Repositories.Interfaces;
using Catalog.Responses.Product;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Handlers.Product;

public class GetAllProductsHandler(IProductRepository productRepository) 
             : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    public async Task<Pagination<ProductResponse>> Handle
                 (GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = 
            await productRepository.GetAllPaginatedProductsAsync(request.CatalogSpecParams);
        var productResponseList = productList.ToResponse();

        return productResponseList;
    }
}
