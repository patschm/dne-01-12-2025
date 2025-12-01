

using Microsoft.Extensions.DependencyInjection;

namespace Onafhankelijkheid;

internal class Program
{
    static void Main(string[] args)
    {
        // LeanAndMean();
        HipEnModern();
    }

    private static void HipEnModern()
    {
        var factory = new DefaultServiceProviderFactory();
        
        var servics = new ServiceCollection();
       servics.AddKeyedScoped<ICounter, Counter>("goed");
        servics.AddKeyedScoped<ICounter, MinCounter>("fout");
        servics.AddTransient<CounterContainer, CounterContainer>();
        servics.AddTransient<MinCounterContainer>();
        

        var builder = factory.CreateBuilder(servics);
        var provider = builder.BuildServiceProvider();

        var container =   provider.GetRequiredService<CounterContainer>();
        container.Rund();

        var container2 = provider.GetRequiredService<MinCounterContainer>();
        container2.Rund();

        //var cnt = provider.GetRequiredService<ICounter>();
        //cnt.Increment();
        //cnt.Increment();
        //cnt.Increment();
        //Console.WriteLine(cnt.Current);


        //var scope = provider.CreateScope();
        //var c1 = scope.ServiceProvider.GetRequiredService<ICounter>();
        //c1.Increment();
        //c1.Increment();
        //c1 = scope.ServiceProvider.GetRequiredService<ICounter>();
        //c1.Increment();
        //scope.Dispose();

        //var scope2 = provider.CreateScope();
        //var c2 = scope2.ServiceProvider.GetRequiredService<ICounter>();
        //c2.Increment();
        //c2 = scope2.ServiceProvider.GetRequiredService<ICounter>();
        //c2.Increment();
        //scope2.Dispose();
        //scope = null;

        //c2.Increment();
    }

    private static void LeanAndMean()
    {
        Counter cnt = new Counter();
        cnt.Increment();
        cnt.Increment();
        cnt.Increment();
        Console.WriteLine(cnt.Current);
    }
}
