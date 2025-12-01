using Microsoft.Extensions.DependencyInjection;

namespace Onafhankelijkheid;

internal class CounterContainer
{
    private readonly ICounter _counter;

    public CounterContainer([FromKeyedServices("goed")]ICounter counter)
    {
        _counter = counter;
    }

    public void Rund()
    {
        _counter.Increment();
        _counter.Increment();
        _counter.Increment();
    }
}
