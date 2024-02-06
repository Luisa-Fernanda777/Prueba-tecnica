using ErrorOr;
using MediatR;
using Products.Common;

namespace Application.Products.GetAll;

public record GetAllProductsQuery() : IRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;