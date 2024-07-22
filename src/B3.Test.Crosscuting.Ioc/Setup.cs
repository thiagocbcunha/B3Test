using B3.Test.Application;
using B3.Test.Domain;
using B3.Test.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Test.Crosscuting.Ioc;

public static class DomainSetup
{
    public static IServiceCollection B3AppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomain();
        services.AddApplication();
        services.AddInfra(configuration);

        return services;
    }
}