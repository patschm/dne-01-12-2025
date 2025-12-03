
using AndereBieb;
using System.Reflection;

namespace TheClient;

internal class Program
{
    static async Task Main(string[] args)
    {
        //var p = new Person { Firstname = "Henk", Lastname = "Pieters", Age = 42 };
        //await p.IntroduceAsync();


        Assembly asm = Assembly.LoadFile(@"D:\DNE\Dist\DeBieb.dll");
        Console.WriteLine(asm.FullName);
        //ShowContent(asm);
        //DoeErIetsMee(asm);
        DeAndereBieb();
        Console.ReadLine();
    }

    private static void DeAndereBieb()
    {
        Person p = new Person();
        IntroduceHandler(p);
    }

    private static void IntroduceHandler(Person p)
    {
        var arrt = p.GetType().GetMethod("Introduce").GetCustomAttribute<MyAttribute>();
        if (arrt.MinAge > 16 && arrt.MaxAge < 70)
        {
            p.Introduce();
        }
        else
        {
            Console.WriteLine("Te oud of te jong");
        }
    }

    private static void DoeErIetsMee(Assembly asm)
    {
        Type? tPerson = asm.GetType("DeBieb.Person");
        object? p1 = Activator.CreateInstance(tPerson!);

        PropertyInfo first = tPerson.GetProperty("Firstname")!;
        PropertyInfo last = tPerson.GetProperty("Lastname")!;
        PropertyInfo age = tPerson.GetProperty("Age")!;

        first.SetValue(p1, "Marieke");
        last.SetValue(p1, "de Vries");
        age.SetValue(p1, -34);

        FieldInfo _age = tPerson.GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic);
        _age.SetValue(p1, -200);

        MethodInfo intro = tPerson.GetMethod("IntroduceAsync")!;
        intro.Invoke(p1, []);

        dynamic? p2 = Activator.CreateInstance(tPerson!);
        p2.Firstname = "Kees";
        p2.Lastname = "Hendriks";
        p2.Age = 42;

        p2.IntroduceAsync();


        Console.WriteLine(p1);
    }

    private static void ShowContent(Assembly asm)
    { 
        foreach(var type in asm.GetTypes())
        {
            Console.WriteLine(string.Join(',', type.GetInterfaces().Select(f=>f.Name).ToArray()));
            Console.WriteLine(type.FullName);
            ShowMembers(type);
        }
    }

    private static void ShowMembers(Type type)
    {
        var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;
        Console.WriteLine("\t=== Fields ====");
        foreach (var field in type.GetFields(flags))
        {
            Console.WriteLine($"\t{field.Name}: {field.FieldType.Name}");
        }
        Console.WriteLine("\t=== Properties ====");
        foreach (var prop in type.GetProperties(flags))
        {
            Console.WriteLine($"\t{prop.Name}: {prop.PropertyType.Name}");
        }
        Console.WriteLine("\t=== Methods ====");
        foreach (var prop in type.GetMethods(flags))
        {
            Console.WriteLine($"\t{prop.Name}: {prop.ReturnType.Name}");
        }
        Console.WriteLine("\t=== Constructor ====");
        foreach (var prop in type.GetConstructors(flags))
        {
            Console.WriteLine($"\t{prop.Name}");
        }
    }
}
