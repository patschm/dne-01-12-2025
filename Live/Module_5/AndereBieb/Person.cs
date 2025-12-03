namespace AndereBieb;

[Obsolete("Niet gebruiken", false)]
public class Person
{

    [My(MinAge =18, MaxAge = 75)]
    public void Introduce()
    {
        Console.WriteLine("Hoi");
    }
}
