using Domain.Products;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Products.Delete;

internal sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _ProductRepository;
    private readonly IUnitOfWorkProduct _unitOfWork;
    public DeleteProductCommandHandler(IProductRepository ProductRepository, IUnitOfWorkProduct unitOfWork)
    {
        _ProductRepository = ProductRepository ?? throw new ArgumentNullException(nameof(ProductRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        if (await _ProductRepository.GetProductByIdAsync(command.Id) is not Product Product)
        {
            return Error.NotFound("Product.NotFound", "The Product with the provide Id was not found.");
        }

        _ProductRepository.Delete(Product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
