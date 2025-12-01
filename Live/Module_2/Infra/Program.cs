
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;

namespace Infra;

internal class Program
{
    static void Main(string[] args)
    {
        ConfigFiles();
        Console.ReadLine();
    }

    private static void ConfigFiles()
    {
        Console.WriteLine(Environment.OSVersion);
        string file = Environment.GetEnvironmentVariable("APP_SET");
        Console.WriteLine(file);
        ConfigurationBuilder bld = new ConfigurationBuilder();
        bld.AddJsonFile(file, true, true);
        

        var app= bld.Build();
        IConfiguration config = app;


        app.GetReloadToken().RegisterChangeCallback(arg => {
            Console.WriteLine("Changed");
            var section = config.GetSection("Section1:Name");
            Console.WriteLine(section.Value);

        }, null);
        
        var section = config.GetSection("Section1:Name");
        string inhoud = section.Value;

        var section1 = config.GetSection("Section1");
        Section? sect = section1.Get<Section>();

        Console.WriteLine(sect.Name);


        Section s1 = new Section();
        section1.Bind(s1);

        Console.WriteLine(s1.Name);
        Console.WriteLine(inhoud);
    }
}
