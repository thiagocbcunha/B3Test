using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.FeeServices;
using B3.Test.Domain.FeeServices.Fees;
using B3.Test.Domain.InvestmentServices;
using B3.Test.Domain.InvestmentServices.Investments;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace B3.Test.Domain;

[ExcludeFromCodeCoverage]
public static class DomainSetup
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ICdiFee, CdiFee>();
        services.AddScoped<IFeeFactory, FeeFactory>();
        services.AddScoped<IFeeService, FeeService>();
        services.AddScoped<ICdbInvestment, CdbInvestment>();
        services.AddScoped<IInvestmentFactory, InvestmentFactory>();
        services.AddScoped<IInvestmentService, InvestmentService>();

        return services;
    }
}