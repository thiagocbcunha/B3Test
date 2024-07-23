using System.Diagnostics;
using B3.Test.Library.Options;
using B3.Test.Library.Contracts;

namespace B3.Test.Library.Tracing;

public class EnterpriseActivityFactory(EnterpriceConfiguration config) : IActivity, IActivityFactory
{
    private Activity? _activity;
    private EnterpriseActivityTag? _tag;

    public IActivity Start<TCaller>()
    {
        return Start(typeof(TCaller).Name);
    }

    public IActivity Start(string identify)
    {
        var activitySource = new ActivitySource(config.Name, config.Version);
        _activity = activitySource.StartActivity(identify);

        if (_activity is not null)
            _tag = new EnterpriseActivityTag(_activity);

        return this;
    }

    public void Dispose()
    {
        _activity?.Dispose();
        _activity = null;
    }

    public ITag? Tag => _tag;
}