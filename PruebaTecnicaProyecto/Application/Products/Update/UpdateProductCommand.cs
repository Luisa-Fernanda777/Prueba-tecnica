using Domain.Organizations;
using ErrorOr;
using MediatR;

namespace Application.Products.Update;

public record UpdateProductCommand(
    int Id,
    string Name,
    int Price,
    int OrganizationId
) : IRequest<ErrorOr<Unit>>;