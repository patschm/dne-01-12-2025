using Microsoft.Extensions.DependencyInjection;

namespace Onafhankelijkheid;

internal class MinCounterContainer
{
    private readonly ICounter _counter;

    public MinCounterContainer([FromKeyedServices("fout")] ICounter counter)
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
