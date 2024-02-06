using Application.Data;
using Domain.Users;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Domain.Organizations;
using Domain.Products;

namespace Infrastucture.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork{
    
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options){
        
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
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