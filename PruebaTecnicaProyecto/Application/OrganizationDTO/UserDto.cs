namespace Application.OrganizationDto;

public class UserDto
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    // Clave foránea para la relación con Organization
    public int OrganizationId { get; set; }
}