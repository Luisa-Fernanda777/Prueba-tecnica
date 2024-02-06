using ErrorOr;
using MediatR;

namespace Application.Products.Delete;

public record DeleteProductCommand(int Id) : IRequest<ErrorOr<Unit>>;