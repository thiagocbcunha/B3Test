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
    public async Task<InvestmentModel> GetInvestment(InvestmentRequestModel investmentRequest)
    {
        activityFactory.Start<CdbInvestment>()
            .Tag?.SetTag("log", "Executing GetInvestment");

        logger.LogInformation("Executing GetInvestment");

        var now = DateTime.Now;
        var initValue = investmentRequest.InitialInvestment;

        var freeTax = initValue;
        var investmentModel = new InvestmentModel();
        var CDI = new BasicFeeModel(investmentRequest.Fee);
        var TB = await profitabilityRepository.GetByInvestmentType(InvestmentEnum.CDB);

        if (CDI.Fee == 0)
            CDI = await feeService.GetCurrent(FeeEnum.CDI);

        investmentModel.Fee = CDI.Fee;
        investmentModel.InvestmentPaid = TB.Paid;
        investmentModel.Type = InvestmentEnum.CDB;
        investmentModel.Performance = initValue;
        investmentModel.ProfitFreeIR = initValue;

        for (int month = 1; month < investmentRequest.TimeInvestmentInMonth + 1; month++)
        {
            var tax = GetTax(month);
            initValue = CalcProfit(initValue, CDI.RealFee, TB.RealPaid);
            investmentModel.Performance = initValue;

            var profit = initValue - freeTax;
            profit -= (profit * tax);

            investmentModel.ProfitFreeIR = freeTax + profit;
            investmentModel.PerformanceByMonth.Add(new InvestmentMonthModel(Prevision: now.AddMonths(month + 1), Tax: tax, Performance: initValue, ProfitFreeIR: investmentModel.ProfitFreeIR));
        }

        return investmentModel;
    }

    private static decimal CalcProfit(decimal initialValue, decimal fee, decimal valuePaid)
    {
        return initialValue * (1 + (fee * valuePaid));
    }

    private static decimal GetTax(int time)
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