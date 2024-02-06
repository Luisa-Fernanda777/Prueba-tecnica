using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain.Organizations;
using Domain.Primitives;
using Domain.Products;
using ErrorOr;
using MediatR;

namespace Application.Products.Create;

internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Unit>>{
    private readonly IProductRepository _ProductRepository;
    private readonly IUnitOfWorkProduct _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWorkProduct unitOfWork, IProductRepository ProductRepository){
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _ProductRepository = ProductRepository ?? throw new ArgumentNullException(nameof(ProductRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(CreateProductCommand request, CancellationToken cancellationToken){

        try
        {
            var Product = new Product(
                new int(),
                request.Name,
                request.Price,
                request.OrganizationId
        );

        _ProductRepository.Add(Product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Failure("Create Product Error", ex.Message);
        }
    }
}