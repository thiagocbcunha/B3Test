using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace B3.Test.Application.Command;

[ExcludeFromCodeCoverage]
public class InvestmentCommand : IRequest<InvestmentModel>
{
    [Required(ErrorMessage = "required value.")]
    [Range(1, 1999999, ErrorMessage = "value out range.")]
    public decimal InitialInvestment { get; set; }

    [Required(ErrorMessage = "required value.")]
    [Range(1, 360, ErrorMessage = "value out range.")]
    public int TimeInvestmentInMonth { get; set; }
    public InvestmentEnum InvestmentEnum { get; set; }

    public static explicit operator InvestmentRequestModel(InvestmentCommand investmentCommand)
    {
        return new InvestmentRequestModel(investmentCommand.TimeInvestmentInMonth, investmentCommand.InitialInvestment, investmentCommand.InvestmentEnum);
    }
}