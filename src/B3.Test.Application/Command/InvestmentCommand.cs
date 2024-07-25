using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace B3.Test.Application.Command;

[ExcludeFromCodeCoverage]
public class InvestmentCommand : IRequest<InvestmentModel>
{
    public decimal Fee { get; set; } = 0;

    [Required(ErrorMessage = "Value of attribute required.")]
    [Range(1, 1999999, ErrorMessage = "Value out of range. The value should be between 1 1999999.")]
    public decimal InitialInvestment { get; set; }

    [Required(ErrorMessage = "Value of attribute required.")]
    [Range(2, 360, ErrorMessage = "Value out of range. The value should be between 2 and 360.")]
    public int TimeInvestmentInMonth { get; set; }
    public InvestmentType InvestmentType { get; set; }

    public static explicit operator InvestmentRequestModel(InvestmentCommand investmentCommand)
    {
        return new InvestmentRequestModel(investmentCommand.Fee, investmentCommand.TimeInvestmentInMonth, investmentCommand.InitialInvestment, investmentCommand.InvestmentType);
    }
}