
using Prueba.API.Middlewares;

namespace Prueba.API;

public static class Startup
{
    public static IServiceCollection AddPresentation(this IServiceCollection services){
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        return services;
    }
}