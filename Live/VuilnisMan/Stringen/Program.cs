namespace Stringen;

internal class Program
{
    static void Main(string[] args)
    {
        string s = "";

        for(int i = 0; i < 100000; i++)
        {
            s += i.ToString();
        }

        Console.ReadLine();
    }
}
