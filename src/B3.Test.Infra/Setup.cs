using B3.Test.Domain.Core.Contracts.Acl;
using B3.Test.Domain.Core.Contracts.Repositories;
using B3.Test.Infra.ACL.BC;
using B3.Test.Infra.ACL.IPEA;
using B3.Test.Infra.Database;
using B3.Test.Infra.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Test.Infra;

public static class DomainSetup
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var souceFee = new SourceFee();
        configuration.GetRequiredSection(nameof(SourceFee)).Bind(souceFee);

        services.AddScoped(i => souceFee);
        services.AddScoped<IDailyCDIAcl, BCFeeAcl>();
        services.AddScoped<IMonthlyCDIAcl, IpeaFeeAcl>();
        services.AddScoped<IProfitabilityRepository, ProfitabilityRepositoryMock>();

        return services;
    }
}