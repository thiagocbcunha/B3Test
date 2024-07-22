using B3.Test.Domain.Core.Contracts.Acl;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.FeeServices.Fees;

public class CDIFee(IDailyCDIAcl _dailyCDIAcl, IMonthlyCDIAcl _monthlyCDIAcl) : ICDIFee
{
    public async Task<BasicFeeModel> GetConsolidated()
    {
        var dailyFee = await _dailyCDIAcl.GetFees();
        var monthlyFee = await _monthlyCDIAcl.GetFees();
        var initMonthDate = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 2).Date;

        var fee = dailyFee.Where(i => i.Date > initMonthDate.AddDays(-1)).Sum(i => i.Fee);
        fee += monthlyFee.Where(i => i.Date > initMonthDate.AddYears(-1)).Sum(i => i.Fee);

        return new BasicFeeModel(fee);
    }

    public async Task<BasicFeeModel> GetCurrent()
    {
        var monthlyFee = await _monthlyCDIAcl.GetFees();
        var fee = monthlyFee.Last().Fee;

        return new BasicFeeModel(fee);
    }
}