using Products.Common;
using Domain.Products;
using MediatR;
using ErrorOr;

namespace Application.Products.GetById;


internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _productRepository.GetProductByIdAsync(query.Id) is not Product product)
        {
            return Error.NotFound("Product.NotFound", "The product with the provide Id was not found.");
        }

        return new ProductResponse(
            product.Id, 
            product.Name,
            product.Price,
            product.OrganizationId);
    }
}