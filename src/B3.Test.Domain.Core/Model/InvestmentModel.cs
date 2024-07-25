using B3.Test.Domain.Core.Types;

namespace B3.Test.Domain.Core.Model;

public class InvestmentModel
{
    public string Name { get; set; } = "";
    public decimal Fee { get; set; }
    public decimal InvestmentPaid { get; set; }
    public InvestmentType Type { get; set; }
    public decimal Performance { get; set; }
    public decimal ProfitFreeIR { get; set; }
    public IList<InvestmentMonthModel> PerformanceByMonth { get; set; } = new List<InvestmentMonthModel>();
}