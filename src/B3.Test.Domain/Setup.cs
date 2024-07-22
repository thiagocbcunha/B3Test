using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.FeeServices;
using B3.Test.Domain.FeeServices.Fees;
using B3.Test.Domain.InvestmentServices;
using B3.Test.Domain.InvestmentServices.Investments;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Test.Domain;

public static class DomainSetup
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ICDIFee, CDIFee>();
        services.AddScoped<IFeeFactory, FeeFactory>();
        services.AddScoped<IFeeService, FeeService>();
        services.AddScoped<ICDBInvestment, CDBInvestment>();
        services.AddScoped<IInvestmentFactory, InvestmentFactory>();
        services.AddScoped<IInvestmentService, InvestmentService>();

        return services;
    }
}