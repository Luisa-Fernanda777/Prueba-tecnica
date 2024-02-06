using Products.Common;
using Domain.Products;
using MediatR;
using ErrorOr;

namespace Application.Products.GetAll;


internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<IReadOnlyList<ProductResponse>>>{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository){
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<ProductResponse>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken){
        IReadOnlyList<Product> products = await _productRepository.GetAll();

        return products.Select(product => new ProductResponse(
                product.Id,
                product.Name,
                product.Price,
                product.OrganizationId
            )).ToList();
            
    }
}