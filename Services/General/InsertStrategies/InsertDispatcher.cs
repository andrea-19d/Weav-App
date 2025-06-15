namespace Weav_App.Services.General.InsertStrategies;

public class InsertDispatcher
{
    private readonly IServiceProvider _provider;

    public InsertDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IStrategy<T> Resolve<T>()
    {
        return _provider.GetRequiredService<IStrategy<T>>();
    }
}
