using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Primitives;
using Domain.Products;
using Domain.Users;

namespace Domain.Organizations;

public class Organization : AggregateRoot{
    public Organization(int id, string name, string slugtenant){
        Id = id;
        Name = name;
        SlugTenant = slugtenant;
    }

    public Organization(){

    }

    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? SlugTenant { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Product> Products { get; set; } = new List<Product>(); // Relacion uno a muchos entre Organization y Products

    [JsonIgnore]
    public List<User> Users { get; set; } = new List<User>();// Relacion uno a muchos entre Organization y Users

}