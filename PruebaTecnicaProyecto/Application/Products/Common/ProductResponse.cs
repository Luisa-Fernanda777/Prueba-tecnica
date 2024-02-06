using Domain.Organizations;

namespace Products.Common;

public record ProductResponse(
int Id,
string Name,
int Price,
int Organization);
