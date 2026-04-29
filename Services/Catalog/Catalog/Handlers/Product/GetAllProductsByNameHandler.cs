using Catalog.Mappers;
using Catalog.Queries.Product;
using Catalog.Repositories.Interfaces;
using Catalog.Responses.Product;
using MediatR;

namespace Catalog.Handlers.Product;

public record GetAllProductsByNameHandler(IProductRepository productRepository)
              : IRequestHandler<GetAllProductsByNameQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetAllProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var productList =
            await productRepository.GetAllProductsByNameAsync(request.Name);

        var productResponseList = productList.ToResponseList().ToList();

        return productResponseList;
    }
}
