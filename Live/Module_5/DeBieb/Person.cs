namespace DeBieb;

public class Person : IIntroducable
{
    private int _age;

    public int Age
    {
        get { return _age; }
        set
        {
            if (value > 0 && value <= 123)
                _age = value;
        }
    }

    public string? Firstname { get; set; }
    public string? Lastname { get; set; }

    public async Task IntroduceAsync()
    {
        Console.WriteLine($"{Firstname} {Lastname} ({Age})");
        await Task.CompletedTask;
    }
}
