using B3.Test.Application.Command;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Test.Application;

public static class DomainSetup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<InvestmentCommand>());

        return services;
    }
}