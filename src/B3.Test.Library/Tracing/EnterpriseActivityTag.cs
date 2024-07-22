using System.Diagnostics;
using B3.Test.Library.Contracts;

namespace B3.Test.Library.Tracing;

public class EnterpriseActivityTag(Activity activity) : ITag
{
    public ITag SetTag(string key, object value)
    {
        activity.SetTag(key, value);
        return this;
    }
}