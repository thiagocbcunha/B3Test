using B3.Test.Domain.Core.Model;
using B3.Test.Library.Contracts;
using B3.Test.Domain.Core.Enums;
using Microsoft.Extensions.Logging;
using B3.Test.Domain.Core.Contracts.Repositories;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;

namespace B3.Test.Domain.InvestmentServices.Investments;

public class CdbInvestment(ILogger<CdbInvestment> logger, IActivityFactory activityFactory, IFeeService feeService, IProfitabilityRepository profitabilityRepository) : ICdbInvestment
{
    public async Task<InvestmentModel> GetInvestment(int timeOfInvestment, decimal initValue)
    {
        activityFactory.Start<CdbInvestment>()
            .Tag?.SetTag("log", "Executing GetInvestment");

        logger.LogInformation("Executing GetInvestment");

        var now = DateTime.Now;
        var freeTax = initValue;
        var investmentModel = new InvestmentModel();
        var CDI = await feeService.GetCurrent(FeeEnum.CDI);
        var TB = await profitabilityRepository.GetByInvestmentType(InvestmentEnum.CDB);

        investmentModel.Fee = CDI.Fee;
        investmentModel.InvestmentPaid = TB.Paid;
        investmentModel.Type = InvestmentEnum.CDB;
        investmentModel.Performance = initValue;
        investmentModel.TaxExemptProfit = initValue;

        for (int month = 0; month < timeOfInvestment; month++)
        {
            var tax = GetTax(month);
            initValue = CalcProfit(initValue, CDI.RealFee, TB.RealPaid);
            investmentModel.Performance = initValue;

            var profit = initValue - freeTax;
            profit = profit - (profit * tax);

            investmentModel.TaxExemptProfit = freeTax + profit;
            investmentModel.PerformanceByMonth.Add(new InvestmentMonthModel(Prevision: now.AddMonths(month + 1), Performance: initValue, TaxExemptProfit: investmentModel.TaxExemptProfit));
        }

        return investmentModel;
    }

    private decimal CalcProfit(decimal initialValue, decimal fee, decimal valuePaid)
    { 
        return initialValue * (1 + (fee * valuePaid ));
    }

    private decimal GetTax(int time)
    {
        if (time <= 6)
            return 0.225m;

        else if (time <= 12)
            return 0.2m;

        else if (time <= 24)
            return 0.175m;

        return 0.15m;
    }
}