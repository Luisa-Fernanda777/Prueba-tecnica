using System.Text.Json.Serialization;
using Domain.Organizations;
using Domain.Primitives;

namespace Domain.Users;

public class User : AggregateRoot{

    public User(int id, string email, string password, int organizationId){
        Id = id;
        Email = email;
        Password = password;
        OrganizationId = organizationId;
    }

    public User(){
    }

    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    // Clave foránea para la relación con Organization
    public int OrganizationId { get; set; }

    [JsonIgnore]
    public Organization Organization { get; set; }
}