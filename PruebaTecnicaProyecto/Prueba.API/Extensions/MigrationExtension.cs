using Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Prueba.API.Extension;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication app){
        using var scope = app.Services.CreateScope();

        var dbUserOrganizationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dbProductContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

        dbUserOrganizationContext.Database.Migrate();
        dbProductContext.Database.Migrate();
    }
}