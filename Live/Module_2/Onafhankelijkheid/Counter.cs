using System;
using System.Collections.Generic;
using System.Text;

namespace Onafhankelijkheid;

internal class Counter : ICounter
{
    private int _counter = 0;

    public int Current { get => _counter; }
    public void Increment()
    {
        Console.WriteLine($"Verhoog {++_counter}");
    }
}
