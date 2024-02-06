using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public class MigrationHelper
{
    public static void ApplyMigrations<TContext>(TContext context) where TContext : DbContext{
        context.Database.Migrate();
    }
}