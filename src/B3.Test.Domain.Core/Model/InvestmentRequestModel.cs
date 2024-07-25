using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.Core.Model;

public record InvestmentRequestModel(decimal Fee, int TimeInvestmentInMonth, decimal InitialInvestment, InvestmentEnum InvestmentEnum);