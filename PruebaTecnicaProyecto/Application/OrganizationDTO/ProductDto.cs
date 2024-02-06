namespace Application.OrganizationDto;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; } = 0;
    public int OrganizationId { get; set; }
}