using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Test.Library.Tracing;

[ExcludeFromCodeCoverage]
public static class EnterpriseTracingBuilderExtension
{
    public static EnterpriseTracingBuilder CreateEnterpriseTracingBuilder(this IServiceCollection service, IConfiguration configuration)
        => new EnterpriseTracingBuilder(service, configuration);
}
