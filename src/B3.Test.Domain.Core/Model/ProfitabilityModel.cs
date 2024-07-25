using B3.Test.Domain.Core.Types;

namespace B3.Test.Domain.Core.Model;

public record ProfitabilityModel(InvestmentType InvestmentType, decimal Paid)
{
    public decimal RealPaid => Paid / 100;
}