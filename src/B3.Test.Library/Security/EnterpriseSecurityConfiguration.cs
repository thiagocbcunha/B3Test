using B3.Test.Library.Contracts;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Test.Library.Security;

[ExcludeFromCodeCoverage]
public static class EnterpriseSecurityConfiguration
{
    public static IServiceCollection AddEnterpriseSecurity(this IServiceCollection services)
    {
        services.AddScoped<IEnterpriseSecurity, EnterpriseSecurity>();

        return services;
    }
}
