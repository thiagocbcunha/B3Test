using B3.Test.Domain.Core.Types;
using B3.Test.Domain.Core.Model;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace B3.Test.Application.Command;

[ExcludeFromCodeCoverage]
public record ProfitabilityCommand(InvestmentType InvestmentType) : IRequest<ProfitabilityModel>;