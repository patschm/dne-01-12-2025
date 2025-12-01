namespace Onafhankelijkheid;

internal interface ICounter
{
    int Current { get; }

    void Increment();
}