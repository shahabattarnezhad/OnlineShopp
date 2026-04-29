using Catalog.Mappers;
using Catalog.Queries.Product;
using Catalog.Repositories.Interfaces;
using Catalog.Responses.Product;
using MediatR;

namespace Catalog.Handlers.Product;

public record GetProductByIdHandler(IProductRepository ProductRepository) 
              : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetProductByIdAsync(request.Id);
        var productResponse = product.ToResponse();

        return productResponse;
    }
}
