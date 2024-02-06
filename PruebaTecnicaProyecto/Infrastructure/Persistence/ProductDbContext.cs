using Application.Data;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Domain.Products;
using Domain.Organizations;
using Domain.Users;

namespace Infrastucture.Persistence;

public class ProductDbContext : DbContext, IProductDbContext, IUnitOfWorkProduct{
    
    private readonly IPublisher _publisher;

    public ProductDbContext(DbContextOptions<ProductDbContext> options, IPublisher publisher) : base(options){
        
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Product>();
        modelBuilder.Entity<Organization>();
        modelBuilder.Ignore<User>();
        modelBuilder.Entity<Organization>()
            .Ignore(o => o.Products)
            .Ignore(o => o.Users);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()){

        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany (e => e.GetDomainEvents());
        
        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents){
            
            await _publisher.Publish(domainEvents, cancellationToken);

        }
        return result;
    }
}

