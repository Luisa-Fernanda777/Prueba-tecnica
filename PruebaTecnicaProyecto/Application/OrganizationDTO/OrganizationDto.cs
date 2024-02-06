using Domain.Organizations;
using Domain.Products;
using Domain.Users;
namespace Application.OrganizationDto;

public class OrganizationDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SlugTenant { get; set; } = string.Empty;
    public List<ProductDto> Products { get; set; } = new List<ProductDto>(); // Relacion uno a muchos entre Organization y Products
    public List<UserDto> Users { get; set; } = new List<UserDto>();// Relacion uno a muchos entre Organization y Users

}