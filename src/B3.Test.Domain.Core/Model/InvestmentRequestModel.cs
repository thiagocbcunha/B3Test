using B3.Test.Domain.Core.Enums;

namespace B3.Test.Domain.Core.Model;

public record InvestmentRequestModel(int TimeInvestmentInMonth, decimal InitialInvestment, InvestmentEnum InvestmentEnum);