using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace B3.Test.Library.Logging;

public static class EnterpriceLogConfiguration
{
    public static void ConfigureEnterpriceLog(this ILoggingBuilder loggingBuilder, IConfiguration configuration, string appNameSection)
    {
        var logBulder = new EnterpriseLoggerBuilder(configuration);

        loggingBuilder.ClearProviders();

        logBulder
            .AddEnrich()
            .ApplyFilter()
            .WriteToConsole()
            .WriteToLogstash()
            .AddName(appNameSection);


        loggingBuilder.AddSerilog(logBulder.Build());
    }
}