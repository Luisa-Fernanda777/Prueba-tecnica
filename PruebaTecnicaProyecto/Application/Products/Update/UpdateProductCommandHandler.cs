using Domain.Products;
using Domain.Primitives;
using MediatR;
using ErrorOr;

namespace Application.Products.Update;

internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWorkProduct _unitOfWork;
    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWorkProduct unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (!await _productRepository.ExistsAsync(command.Id))
        {
            return Error.NotFound("Product.NotFound", "The product with the provide Id was not found.");
        }

        Product product = Product.UpdateProduct(command.Id, command.Name,
            command.Price,
            command.OrganizationId);

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}