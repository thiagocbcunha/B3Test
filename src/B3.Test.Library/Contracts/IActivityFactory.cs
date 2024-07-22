namespace B3.Test.Library.Contracts;

public interface IActivityFactory
{
    ITag? Tag { get; }
    IActivity Start<TCaller>();
    IActivity Start(string identify);
}