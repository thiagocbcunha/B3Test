using B3.Test.Domain.Core.Types;

namespace B3.Test.Domain.Core.Model;

public record InvestmentRequestModel(decimal Fee, int TimeInvestmentInMonth, decimal InitialInvestment, InvestmentType InvestmentType);