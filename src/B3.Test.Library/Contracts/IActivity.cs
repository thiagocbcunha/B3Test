namespace B3.Test.Library.Contracts;

public interface IActivity : IDisposable
{
    ITag? Tag { get; }
}