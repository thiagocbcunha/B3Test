namespace B3.Test.Domain.Core.Model;

public record BasicFeeModel(decimal Fee)
{
    public decimal RealFee => Fee / 100;
}