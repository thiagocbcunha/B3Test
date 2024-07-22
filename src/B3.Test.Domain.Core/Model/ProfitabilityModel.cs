using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.Core.Model;

public record ProfitabilityModel(InvestmentEnum InvestmentEnum, decimal Paid)
{
    public decimal RealPaid => Paid / 100;
}