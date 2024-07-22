using B3.Test.Library.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Test.Library.Security;

public static class EnterpriseSecurityConfiguration
{
    public static IServiceCollection AddEnterpriseSecurity(this IServiceCollection services)
    {
        services.AddScoped<IEnterpriseSecurity, EnterpriseSecurity>();

        return services;
    }
}
