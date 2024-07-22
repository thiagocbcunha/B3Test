using B3.Test.Domain.Core.Contracts.Repositories;
using B3.Test.Domain.Core.Contracts.Services.FeeServices;
using B3.Test.Domain.Core.Contracts.Services.InvestmentServices;
using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.InvestmentServices.Investments;

public class CDBInvestment(IFeeService _feeService, IProfitabilityRepository _profitabilityRepository) : ICDBInvestment
{
    public async Task<InvestmentModel> GetInvestment(int timeOfInvestmentInMonth, decimal initValue)
    {
        var now = DateTime.Now;
        var freeTax = initValue;
        var investmentModel = new InvestmentModel();
        var CDI = await _feeService.GetCurrent(FeeEnum.CDI);
        var TB = await _profitabilityRepository.GetByInvestmentType(InvestmentEnum.CDB);

        investmentModel.Fee = CDI.Fee;
        investmentModel.InvestmentPaid = TB.Paid;
        investmentModel.Type = InvestmentEnum.CDB;
        investmentModel.Performance = initValue;
        investmentModel.TaxExemptProfit = initValue;

        for (int month = 0; month < timeOfInvestmentInMonth; month++)
        {
            var tax = GetTax(month);
            initValue = initValue * (1 + (CDI.RealFee * TB.RealPaid));
            investmentModel.Performance = initValue;
            investmentModel.TaxExemptProfit = initValue - ((initValue - freeTax) * tax);
            investmentModel.PerformanceByMonth.Add(new InvestmentMonthModel(Prevision: now.AddMonths(month + 1), Performance: initValue, TaxExemptProfit: investmentModel.TaxExemptProfit));
        }

        return investmentModel;
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