using B3.Test.Domain.Core.Enums;
using B3.Test.Domain.Core.Model;
using MediatR;

namespace B3.Test.Application.Command;

public record FeeCommand(FeeEnum FeeEnum) : IRequest<BasicFeeModel>;