using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Acl;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;

namespace B3.Test.Domain.FeeServices.Fees;

public class CdiFee(ILogger<CdiFee> logger, IActivityFactory activityFactory, IDailyCDIAcl dailyCDIAcl, IMonthlyCDIAcl monthlyCDIAcl) : ICdiFee
{
    public async Task<BasicFeeModel> GetConsolidated()
    {
        activityFactory.Start<CdiFee>()
            .Tag?.SetTag("log", "Executing GetConsolidated");

        logger.LogInformation("Executing GetConsolidated");

        var dailyFee = await dailyCDIAcl.GetFees();
        var monthlyFee = await monthlyCDIAcl.GetFees();
        var initMonthDate = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 2).Date;

        var fee = dailyFee.Where(i => i.Date > initMonthDate.AddDays(-1)).Sum(i => i.Fee);
        fee += monthlyFee.Where(i => i.Date > initMonthDate.AddYears(-1)).Sum(i => i.Fee);

        return new BasicFeeModel(fee);
    }

    public async Task<BasicFeeModel> GetCurrent()
    {
        activityFactory.Start<CdiFee>()
            .Tag?.SetTag("log", "Executing GetCurrent");

        logger.LogInformation("Executing GetCurrent");

        var monthlyFee = await monthlyCDIAcl.GetFees();
        var fee = monthlyFee.Last().Fee;

        return new BasicFeeModel(fee);
    }
}