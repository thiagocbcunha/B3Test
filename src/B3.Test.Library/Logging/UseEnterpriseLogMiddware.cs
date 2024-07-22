using Microsoft.AspNetCore.Builder;

namespace B3.Test.Library.Logging;

public static class EnterpriseLogMiddware
{
    public static IApplicationBuilder AddEnterpriseLogMiddware(this IApplicationBuilder app)
    {
        app.UseMiddleware<UnhandledExceptionMiddware>();
        return app;
    }
}