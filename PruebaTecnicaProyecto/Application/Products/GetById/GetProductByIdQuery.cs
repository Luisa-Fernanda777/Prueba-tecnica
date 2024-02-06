using ErrorOr;
using MediatR;
using Products.Common;

namespace Application.Products.GetById;

public record GetProductByIdQuery(int Id) : IRequest<ErrorOr<ProductResponse>>;