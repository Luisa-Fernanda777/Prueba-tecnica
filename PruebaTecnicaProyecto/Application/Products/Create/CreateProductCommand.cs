using Domain.Organizations;
using ErrorOr;
using MediatR;

namespace Application.Products.Create;

public record CreateProductCommand(
    string Name,
    int Price,
    int OrganizationId
) : IRequest<ErrorOr<Unit>>;