using EtlProject.Middleware;

namespace EtlProject.ServiceCollection;

public static class CustomMiddlewareConfiguration
{
    public static IApplicationBuilder ConfigureCustomMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}