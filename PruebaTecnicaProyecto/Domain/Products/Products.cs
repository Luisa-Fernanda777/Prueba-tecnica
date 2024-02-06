using System.Text.Json.Serialization;
using Domain.Organizations;
using Domain.Primitives;

namespace Domain.Products;

public class Product : AggregateRoot{
    public Product(int id, string name, int price, int organizationId){
        Id = id;
        Name = name;
        Price = price;
        OrganizationId = organizationId;
    }

    public Product(){

    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; } = 0;
    public int OrganizationId { get; set; }
    [JsonIgnore]
    public Organization Organization { get; set;}

    public static Product UpdateProduct(int id, string name, int price, int organizationId)
    {
        return new Product(id, name, price, organizationId);
    }
}