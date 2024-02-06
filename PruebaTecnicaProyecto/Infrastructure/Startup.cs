using Application.Data;
using Domain.Organizations;
using Domain.Primitives;
using Domain.Products;
using Domain.Users;
using Infrastucture.Persistence;
using Infrastucture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastucture;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration){
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration){
        
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("UserOrganizationDB")));
        services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("ProductOrganization")));
        
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IProductDbContext>(sp => sp.GetRequiredService<ProductDbContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWorkProduct>(sp => sp.GetRequiredService<ProductDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        return services;
    }
}